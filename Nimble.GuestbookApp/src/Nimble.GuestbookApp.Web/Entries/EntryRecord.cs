namespace Nimble.GuestbookApp.Web;

public record EntryRecord(int Id, string EmailAddress, string Message, DateTimeOffset DateTimeCreated);
