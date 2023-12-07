using FastEndpoints;
using Nimble.GuestbookApp.Core.GuestbookAggregate;

namespace Nimble.GuestbookApp.Web.Entries;

public class List : EndpointWithoutRequest<List<GuestbookEntry>>
{
  public override void Configure()
  {
    Get("/Entries");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
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

    await Task.Delay(1);
    Response = guestbook.Entries
      .OrderByDescending(e => e.DateTimeCreated)
      .ToList();
  }

}
