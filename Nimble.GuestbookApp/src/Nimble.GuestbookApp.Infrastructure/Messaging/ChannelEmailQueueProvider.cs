using System.Threading.Channels;
using Nimble.GuestbookApp.Core.Interfaces;
using static Nimble.GuestbookApp.Core.Services.EntryPointService;

namespace Nimble.GuestbookApp.Infrastructure.Messaging;
public class EmailQueueProvider : IEmailQueueProvider
{
  private readonly Channel<EmailDetails> _queue;

  public EmailQueueProvider()
  {
    _queue = Channel.CreateUnbounded<EmailDetails>(); // No max capacity
  }

  public EmailDetails? TryRead()
  {
    _queue.Reader.TryRead(out var message);

    return message;
  }

  public async Task WriteAsync(EmailDetails emailDetails)
  {
    await _queue.Writer.WriteAsync(emailDetails);
  }
}
