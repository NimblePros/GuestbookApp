using Nimble.GuestbookApp.Core.ContributorAggregate;
using Nimble.GuestbookApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Nimble.GuestbookApp.Core.GuestbookAggregate;

namespace Nimble.GuestbookApp.Web;

public static class SeedData
{
  public static readonly Contributor Contributor1 = new ("Ardalis");
  public static readonly Contributor Contributor2 = new ("Snowfrog");

  public static void Initialize(IServiceProvider serviceProvider)
  {
    using (var dbContext = new AppDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
    {
      // Look for any Contributors.
      if (dbContext.Contributors.Any())
      {
        return;   // DB has been seeded
      }

      PopulateTestData(dbContext);
    }
  }
  public static void PopulateTestData(AppDbContext dbContext)
  {
    foreach (var item in dbContext.Contributors)
    {
      dbContext.Remove(item);
    }
    dbContext.SaveChanges();

    dbContext.Contributors.Add(Contributor1);
    dbContext.Contributors.Add(Contributor2);

    dbContext.SaveChanges();

    var guestbook = new Guestbook();
    guestbook.Name = "Default Guestbook";
    dbContext.Guestbooks.Add(guestbook);
    dbContext.SaveChanges();

    var entry1 = new GuestbookEntry()
    {
      DateTimeCreated = new DateTimeOffset(DateTime.Today),
      EmailAddress = "alice@test.com",
      Message = "Hello world!"
    };
    guestbook.Entries.Add(entry1);

    dbContext.SaveChanges();

  }
}
