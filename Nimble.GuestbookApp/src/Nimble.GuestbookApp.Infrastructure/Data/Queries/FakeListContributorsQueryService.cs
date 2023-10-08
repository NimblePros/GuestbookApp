using Nimble.GuestbookApp.UseCases.Contributors;
using Nimble.GuestbookApp.UseCases.Contributors.List;

namespace Nimble.GuestbookApp.Infrastructure.Data.Queries;

public class FakeListContributorsQueryService : IListContributorsQueryService
{
  public Task<IEnumerable<ContributorDTO>> ListAsync()
  {
    var result = new List<ContributorDTO>() { new ContributorDTO(1, "Ardalis"), new ContributorDTO(2, "Snowfrog") };
    return Task.FromResult(result.AsEnumerable());
  }
}

