using SearchSystemDomain.Utilities;
using Xunit;

namespace Tests.IntegrationTests;

public class IntegrationTests : IClassFixture<TestServerFixture>
{
    private readonly TestServerFixture _fixture;

    private readonly string apiBaseUrl;
    public IntegrationTests(TestServerFixture fixture)
    {
        _fixture = fixture;

        apiBaseUrl = EnvironmentConfigHelpers.GetApiUrlWithPort(_fixture.Configuration);
    }



    #region Integration Tests for REST API Endpoints

    [Fact]
    public async Task GetServices_ApiEndpoint_ReturnsResult()
    {
        // Arrange
        using var client = new HttpClient
        {
            BaseAddress = new Uri(apiBaseUrl),
            Timeout = TimeSpan.FromMinutes(5)
        };

        // Act
        var response = await client.GetAsync("/api/services/");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(content));
        Assert.Contains("[", content); // crude check for JSON array
    }

    #endregion
}
