using System.ComponentModel.DataAnnotations;

namespace Nimble.GuestbookApp.Web.Entries;
public class CreateEntryRequest
{
  public const string Route = "/Entries";

  [Required]
  public string? EmailAddress { get; set; }
  [Required]
  public string? Message { get; set; }
}