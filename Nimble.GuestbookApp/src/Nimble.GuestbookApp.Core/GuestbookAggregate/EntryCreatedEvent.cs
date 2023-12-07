using Ardalis.SharedKernel;

namespace Nimble.GuestbookApp.Core.GuestbookAggregate;

internal class EntryCreatedEvent : DomainEventBase
{
  public EntryCreatedEvent(GuestbookEntry entry)
  {
    Entry = entry;
  }
  public GuestbookEntry Entry { get; set; }
}
