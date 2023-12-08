using FluentAssertions;
using Nimble.GuestbookApp.Core.ContributorAggregate;
using Nimble.GuestbookApp.Core.GuestbookAggregate;
using Xunit;

namespace Nimble.GuestbookApp.UnitTests.Core.ContributorAggregate;

public class GuestbookAddEntry
{
  private readonly string _testEmailAddress = "test@test.com";

  private Guestbook CreateGuestbook()
  {
    return new Guestbook();
  }

  [Fact]
  public void AddsEntryToEntriesList()
  {
    var newEntry = new GuestbookEntry
    {
      EmailAddress = _testEmailAddress,
      Message = "test message"
    };

    var guestbook = CreateGuestbook();
    guestbook.AddEntry(newEntry);

    guestbook.Entries.Should().Contain(newEntry);
  }

  [Fact]
  public void AddsDomainEventForNewEntry()
  {
    var newEntry = new GuestbookEntry
    {
      EmailAddress = _testEmailAddress,
      Message = "test message"
    };

    var guestbook = CreateGuestbook();
    guestbook.AddEntry(newEntry);

    guestbook.DomainEvents.Should().ContainSingle();
  }
}
