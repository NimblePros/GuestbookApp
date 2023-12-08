using Nimble.GuestbookApp.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Nimble.GuestbookApp.Infrastructure;

public class LoggerAdapter<T> : ILoggerAdapter<T>
{
  private readonly ILogger<T> _logger;

  public LoggerAdapter(ILogger<T> logger)
  {
    _logger = logger;
  }

  public void LogError(Exception ex, string message, params object[] args)
  {
    _logger.LogError(ex, message, args);
  }

  public void LogInformation(string message, params object[] args)
  {
    _logger.LogInformation(message, args);
  }

  public void LogDebug(string message, params object[] args)
  {
    _logger.LogDebug(message, args);
  }

  public void LogWarning(string message, params object[] args)
  {
    _logger.LogWarning(message, args);
  }
}
