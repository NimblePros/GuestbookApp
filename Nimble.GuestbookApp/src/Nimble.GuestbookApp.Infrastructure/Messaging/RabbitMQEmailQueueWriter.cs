using Ardalis.GuardClauses;
using MassTransit;
using Nimble.GuestbookApp.Core.Interfaces;
using Nimble.GuestbookApp.Core.Services;
using static Nimble.GuestbookApp.Core.Services.EntryPointService;

namespace Nimble.GuestbookApp.Infrastructure.Messaging;

public class RabbitMQEmailQueueWriter(IBusControl busControl, 
  RabbitMQSettings settings) : IEmailQueueWriter
{
  private readonly IBusControl _busControl = Guard.Against.Null(busControl);
  private readonly RabbitMQSettings _settings = Guard.Against.Null(settings);

  public async Task WriteAsync(EmailDetails emailDetails)
  {
    string queue = "email_queue";
    var sendToUri = new Uri(
      $"{ _settings.Host }/{ queue }");
    var endpoint = await _busControl.GetSendEndpoint(sendToUri);

    await endpoint.Send(emailDetails);
  }
}
