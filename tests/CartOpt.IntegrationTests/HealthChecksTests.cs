using System.Net;
using CartOpt.IntegrationTests.Infrastructure;
using Testcontainers.PostgreSql;
using Xunit;

namespace CartOpt.IntegrationTests;

public sealed class HealthChecksTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSql =
        new PostgreSqlBuilder("postgres:18.6-alpine")
            .Build();

    private CartOptWebApplicationFactory? _factory;
    private HttpClient? _client;
    private CancellationToken _token;

    public async ValueTask InitializeAsync()
    {
        _token = new CancellationTokenSource(TimeSpan.FromSeconds(30)).Token;
        await _postgreSql.StartAsync(_token);

        _factory =
            new CartOptWebApplicationFactory(
                _postgreSql.GetConnectionString());

        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task ReadinessWhenPostgreSqlIsAvailableReturnsOk()
    {
        var response =
            await _client!.GetAsync("/health/ready", _token);

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);
    }

    [Fact]
    public async Task LivenessWhenApplicationIsRunningReturnsOk()
    {
        var response =
            await _client!.GetAsync("/health/live", _token);

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);
    }


    public async ValueTask DisposeAsync()
    {
        _client?.Dispose();
        _factory?.Dispose();

        await _postgreSql.DisposeAsync();
    }
}
