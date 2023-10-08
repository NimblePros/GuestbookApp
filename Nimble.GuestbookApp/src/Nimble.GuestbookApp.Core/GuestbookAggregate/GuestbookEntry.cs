using Ardalis.SharedKernel;

namespace Nimble.GuestbookApp.Core.GuestbookAggregate;

public class GuestbookEntry : EntityBase
{
    public string EmailAddress { get; set;} = "";
    public string Message { get; set;} = "";
    public DateTimeOffset DateTimeCreated {get; set;} = DateTime.UtcNow;
}
