using Ardalis.HttpClientTestExtensions;
using FluentAssertions;
using Nimble.GuestbookApp.Web.Entries;
using Xunit;

namespace Nimble.GuestbookApp.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class EntriesCreate : IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly HttpClient _client;

  public EntriesCreate(CustomWebApplicationFactory<Program> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnsOneEntry()
  {
    var testEmail = "abc@def.com";
    var request = new CreateEntryRequest() { EmailAddress = testEmail, Message="hi" };
    var content = StringContentHelpers.FromModelAsJson(request);

    var result = await _client.PostAndDeserializeAsync<CreateEntryResponse>(
      CreateEntryRequest.Route, content);

    result.EmailAddress.Should().Be(testEmail);
    result.Id.Should().BeGreaterThan(0);
  }
}
