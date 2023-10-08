using Ardalis.SharedKernel;

namespace Nimble.GuestbookApp.Core.GuestbookAggregate;

public class Guestbook : EntityBase, IAggregateRoot
{
    public string Name { get; set;} = "";
    public List<GuestbookEntry> Entries {get;} = new();
}
