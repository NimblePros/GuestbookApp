using MediatR;
using Microsoft.Extensions.Logging;
using Nimble.GuestbookApp.Core.Interfaces;
using Nimble.GuestbookApp.Core.Services;

namespace Nimble.GuestbookApp.Core.GuestbookAggregate;

internal class NotifyWithMessagingCreatorEntryCreatedHandler(
  ILogger<NotifyCreatorEntryCreatedHandler> logger,
  IEmailQueueProvider emailQueueProvider)
  : INotificationHandler<EntryCreatedEvent>
{
  private readonly ILogger<NotifyCreatorEntryCreatedHandler> _logger = logger;
  private readonly IEmailQueueProvider _emailQueueProvider = emailQueueProvider;

  public async Task Handle(EntryCreatedEvent domainEvent, CancellationToken cancellationToken)
  {
    _logger.LogInformation("Queueing up email to send for {entryId}", domainEvent.Entry.Id);

    var emailDetails = new EmailDetails("recipient@test.com", "noreply@test.com", "Sent via Queue", "body");
    await _emailQueueProvider.WriteAsync(emailDetails);
  }
}
internal class NotifyWithQueueWriterCreatorEntryCreatedHandler(
  ILogger<NotifyCreatorEntryCreatedHandler> logger,
  IEmailQueueWriter emailQueueWriter)
  : INotificationHandler<EntryCreatedEvent>
{
  private readonly ILogger<NotifyCreatorEntryCreatedHandler> _logger = logger;
  private readonly IEmailQueueWriter _emailQueueWriter = emailQueueWriter;

  public async Task Handle(EntryCreatedEvent domainEvent, CancellationToken cancellationToken)
  {
    _logger.LogInformation("(MassTransit) Queueing up email to send for {entryId}", domainEvent.Entry.Id);

    var emailDetails = new EmailDetails("recipient@test.com", "noreply@test.com", "Sent via RabbitMQ", "body");
    await _emailQueueWriter.WriteAsync(emailDetails);
  }
}
