using MediatR;
using Microsoft.Extensions.Logging;
using Nimble.GuestbookApp.Core.Interfaces;

namespace Nimble.GuestbookApp.Core.GuestbookAggregate;

internal class NotifyCreatorEntryCreatedHandler : INotificationHandler<EntryCreatedEvent>
{
  private readonly ILogger<NotifyCreatorEntryCreatedHandler> _logger;
  private readonly IEmailSender _emailSender;

  public NotifyCreatorEntryCreatedHandler(
    ILogger<NotifyCreatorEntryCreatedHandler> logger,
    IEmailSender emailSender)
  {
    _logger = logger;
    _emailSender = emailSender;
  }

  public async Task Handle(EntryCreatedEvent domainEvent, CancellationToken cancellationToken)
  {
    _logger.LogInformation("Handling EntryCreatedEvent for {entryId}", domainEvent.Entry.Id);

    await _emailSender.SendEmailAsync(
        domainEvent.Entry.EmailAddress,
        "donotreply@test.com",
        "Guestbook Entry Accepted",
        $"Your entry with message {domainEvent.Entry.Message} was received"
    );
  }
}
