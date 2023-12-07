namespace Nimble.GuestbookApp.UseCases.Entries;
public record EntryDTO(int Id, string EmailAddress, string Message,
  DateTimeOffset DateTimeCreated);

