using Ardalis.SharedKernel;

namespace Nimble.GuestbookApp.Core.GuestbookAggregate;
public class Guestbook : EntityBase, IAggregateRoot
{
  public string Name { get; set; } = "";
  private List<GuestbookEntry> _entries = new();
  public IEnumerable<GuestbookEntry> Entries => _entries.AsReadOnly();

  public void AddEntry(GuestbookEntry entry)
  {
    _entries.Add(entry);

    RegisterDomainEvent(new EntryCreatedEvent(entry));
  }

  public void SeedEntry(GuestbookEntry entry)
  {
    _entries.Add(entry);
  }
}
