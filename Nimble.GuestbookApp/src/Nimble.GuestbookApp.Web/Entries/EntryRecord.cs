namespace Nimble.GuestbookApp.Web.Entries;

public record EntryRecord(int Id, string EmailAddress, string Message, DateTimeOffset DateTimeCreated);
