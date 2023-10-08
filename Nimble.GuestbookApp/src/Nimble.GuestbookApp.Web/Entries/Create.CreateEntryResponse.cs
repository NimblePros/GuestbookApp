namespace Nimble.GuestbookApp.Web.Entries;

public class CreateEntryResponse
{
  public int Id { get; set; }
  public string EmailAddress { get; set; } = "";
  public string Message { get; set; } = "";
}
