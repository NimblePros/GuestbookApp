using Ardalis.HttpClientTestExtensions;
using FluentAssertions;
using Nimble.GuestbookApp.Infrastructure.Data;
using Nimble.GuestbookApp.Web.Endpoints.ContributorEndpoints;
using Nimble.GuestbookApp.Web.Entries;
using Xunit;

namespace Nimble.GuestbookApp.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class EntriesList(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly HttpClient _client = factory.CreateClient();

  [Fact]
  public async Task ReturnsOneEntry()
  {
    var result = await _client.GetAndDeserializeAsync<EntryListResponse>("/Entries");

    result.Entries.Count().Should().Be(1);
    Assert.Contains(result.Entries, i => i.EmailAddress == SeedData.TestGuestBookEntryEmail);
  }
}
