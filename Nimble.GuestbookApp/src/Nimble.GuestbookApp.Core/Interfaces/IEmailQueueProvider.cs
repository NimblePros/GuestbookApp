using static Nimble.GuestbookApp.Core.Services.EntryPointService;

namespace Nimble.GuestbookApp.Core.Interfaces;

public interface IEmailQueueProvider
{
  Task WriteAsync(EmailDetails emailDetails);
  EmailDetails? TryRead();
}
