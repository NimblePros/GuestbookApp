using Ardalis.GuardClauses;
using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Nimble.GuestbookApp.Core;
using Nimble.GuestbookApp.Infrastructure;
using Nimble.GuestbookApp.Infrastructure.Data;
using FastEndpoints;
using FastEndpoints.Swagger;
using Serilog;
using Nimble.GuestbookApp.Web;
using Nimble.GuestbookApp.Core.Interfaces;
using Autofac.Core;
using MassTransit;
using static MassTransit.Logging.OperationName;
using Nimble.GuestbookApp.Infrastructure.Messaging;
using System.Configuration;
using System.Net.Mail;
using Nimble.GuestbookApp.Core.Services;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => true;
  options.MinimumSameSitePolicy = SameSiteMode.None;
});

string? connectionString = builder.Configuration.GetConnectionString("SqliteConnection");
Guard.Against.Null(connectionString);
builder.Services.AddApplicationDbContext(connectionString);

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o =>
{
  o.ShortSchemaNames = true;
});

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
  config.Services = new List<ServiceDescriptor>(builder.Services);

  // optional - default path to view services is /listallservices - recommended to choose your own path
  config.Path = "/listservices";
});

// wire up email worker service
var workerSettings = new WorkerSettings();
builder.Configuration.Bind(nameof(WorkerSettings), workerSettings);
builder.Services.AddSingleton(workerSettings);
builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

// configure MassTransit with RabbitMQ

var rabbitMQSettings = new RabbitMQSettings();
builder.Configuration.GetSection("RabbitMQ").Bind(rabbitMQSettings);
builder.Services.AddSingleton(rabbitMQSettings);

builder.Services.AddMassTransit(config =>
{
  config.UsingRabbitMq((context, cfg) =>
  {
    cfg.Host(new Uri(rabbitMQSettings.Host), h =>
    {
      h.Username(rabbitMQSettings.Username);
      h.Password(rabbitMQSettings.Password);
    });
    // Additional configuration
    cfg.Publish<EmailDetails>(e =>
    {
      e.ExchangeType = ExchangeType.Direct;
    });
    //ConfigureQueues(cfg, rabbitMQSettings.QueueSettings);
    //cfg.ReceiveEndpoint("my_queue", e =>
    //{
    //  e.Consumer<MyConsumer>();
    //});
  });
});

// keep this at the end of services registration
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultCoreModule());
  containerBuilder.RegisterModule(new AutofacInfrastructureModule(builder.Environment.IsDevelopment()));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
}
else
{
  app.UseDefaultExceptionHandler(); // from FastEndpoints
  app.UseHsts();
}
app.UseFastEndpoints();
app.UseSwaggerGen(); // FastEndpoints middleware

app.UseHttpsRedirection();

SeedDatabase(app);

app.Run();

static void SeedDatabase(WebApplication app)
{
  using var scope = app.Services.CreateScope();
  var services = scope.ServiceProvider;

  try
  {
    var context = services.GetRequiredService<AppDbContext>();
    //                    context.Database.Migrate();
    context.Database.EnsureCreated();
    SeedData.Initialize(services);
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
  }
}

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program
{
}
