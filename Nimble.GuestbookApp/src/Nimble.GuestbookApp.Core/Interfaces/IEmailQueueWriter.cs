using Nimble.GuestbookApp.Core.Services;
using static Nimble.GuestbookApp.Core.Services.EntryPointService;

namespace Nimble.GuestbookApp.Core.Interfaces;

public interface IEmailQueueWriter
{
  Task WriteAsync(EmailDetails emailDetails);
}
