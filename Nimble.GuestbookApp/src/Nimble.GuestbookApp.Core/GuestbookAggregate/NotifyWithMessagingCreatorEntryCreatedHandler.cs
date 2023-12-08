using MediatR;
using Microsoft.Extensions.Logging;
using Nimble.GuestbookApp.Core.Interfaces;
using static Nimble.GuestbookApp.Core.Services.EntryPointService;

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
