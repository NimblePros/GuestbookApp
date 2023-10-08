using Nimble.GuestbookApp.Web.ContributorEndpoints;

namespace Nimble.GuestbookApp.Web.Endpoints.ContributorEndpoints;

public class ContributorListResponse
{
  public List<ContributorRecord> Contributors { get; set; } = new();
}
