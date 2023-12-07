using Nimble.GuestbookApp.Web.ContributorEndpoints;

namespace Nimble.GuestbookApp.Web.Endpoints.ContributorEndpoints;

public class UpdateContributorResponse
{
  public UpdateContributorResponse(ContributorRecord contributor)
  {
    Contributor = contributor;
  }
  public ContributorRecord Contributor { get; set; }
}
