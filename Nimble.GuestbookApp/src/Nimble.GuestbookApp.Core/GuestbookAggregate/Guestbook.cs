using Ardalis.SharedKernel;

namespace Nimble.GuestbookApp.Core;

public class Guestbook : EntityBase
{
    public string Name { get; set;} = "";
    public List<GuestbookEntry> Entries {get;} = new();
}
