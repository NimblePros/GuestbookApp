using Nimble.GuestbookApp.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Nimble.GuestbookApp.Core.Services;

public class EntryPointService : IEntryPointService
{
  private readonly IEmailQueueProvider _emailQueueProvider;
  private readonly IEmailSender _emailSender;
  private readonly ILoggerAdapter<EntryPointService> _logger;


  public EntryPointService(IEmailQueueProvider emailQueueProvider, IEmailSender emailSender, ILoggerAdapter<EntryPointService> logger)
  {
    _emailQueueProvider = emailQueueProvider;
    _emailSender = emailSender;
    _logger = logger;
  }

  public async Task ExecuteAsync()
  {
    _logger.LogDebug("{service} running at: {time}", nameof(EntryPointService), DateTimeOffset.Now);

    try
    {
      _logger.LogDebug("Queue Receiver: {0}", _emailQueueProvider!.GetType()!.FullName!);

      var details = _emailQueueProvider.TryRead();

      if (details is null)
      {
        return;
      }

      _logger.LogInformation("Got message: {details}", details);
      await _emailSender.SendEmailAsync(details.To, details.From, details.Subject, details.Body);
      _logger.LogInformation("Email Sent: {details}!", details);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, $"{nameof(EntryPointService)}.{nameof(ExecuteAsync)} threw an exception.");
      //throw;
    }
  }
}
