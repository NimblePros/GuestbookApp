namespace Nimble.GuestbookApp.Core.Interfaces;
public interface ILoggerAdapter<T>
{
  void LogDebug(string message, params object[] args);
  void LogError(Exception ex, string message, params object[] args);
  void LogInformation(string message, params object[] args);
  void LogWarning(string message, params object[] args);
}
