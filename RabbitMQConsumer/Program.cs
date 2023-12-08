using MassTransit;
using Nimble.GuestbookApp.Core.Services;
using RabbitMQConsumer;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<MimeKitEmailSender>();
builder.Services.AddHostedService<Worker>();

// set up MassTransit / RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<EmailMessageConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("email_queue", e =>
        {
            e.ConfigureConsumer<EmailMessageConsumer>(context);
        });
        cfg.Message<EmailDetails>(e =>
        {
            e.SetEntityName("email_queue");
        });
    });
});

var host = builder.Build();
host.Run();
