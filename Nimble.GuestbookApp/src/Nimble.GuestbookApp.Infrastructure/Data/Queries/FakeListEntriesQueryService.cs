using Nimble.GuestbookApp.Core.GuestbookAggregate;
using Nimble.GuestbookApp.UseCases.Entries;

namespace Nimble.GuestbookApp.Infrastructure.Data.Queries;
public class FakeListEntriesQueryService : IListEntriesQueryService
{
  public Task<IEnumerable<EntryDTO>> ListAsync()
  {
    var guestbook = new Guestbook();

    guestbook.Entries.Add(new GuestbookEntry()
    {
      DateTimeCreated = new DateTime(2024, 1, 1),
      EmailAddress = "alice@test.com",
      Message = "Hello world!",
    });
    guestbook.Entries.Add(new GuestbookEntry()
    {
      DateTimeCreated = new DateTime(2024, 2, 14),
      EmailAddress = "bob@test.com",
      Message = "Happy Valentine's Day!",
    });
    guestbook.Entries.Add(new GuestbookEntry()
    {
      DateTimeCreated = new DateTime(2024, 6, 20),
      EmailAddress = "carol@test.com",
      Message = "Happy summer!",
    });
    return Task.FromResult(guestbook.Entries
      .OrderByDescending(e => e.DateTimeCreated)
      .Select(e => new EntryDTO() { Id =e.Id, EmailAddress = e.EmailAddress, Message = e.Message, DateTimeCreated = e.DateTimeCreated}));
  }
}