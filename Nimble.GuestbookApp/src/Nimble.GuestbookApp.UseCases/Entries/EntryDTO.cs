namespace Nimble.GuestbookApp.UseCases.Entries;

// Dapper doesn't like this record type apparently
// public record EntryDTO(int Id, string EmailAddress, string Message, 
// DateTimeOffset DateTimeCreated);

public class EntryDTO{
    public int Id {get; set;}
    public string EmailAddress {get; set;} = "";
    public string Message {get; set;} = "";
    public DateTimeOffset DateTimeCreated {get; set;}
}