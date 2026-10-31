using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CartOpt.IntegrationTests.Infrastructure;

public sealed class CartOptWebApplicationFactory(
    string postgreSqlConnectionString)
    : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureHostConfiguration(configuration =>
        {
            configuration.AddInMemoryCollection(
                new Dictionary<string, string?>
                {
                    ["ConnectionStrings:PostgreSql"] =
                        postgreSqlConnectionString
                });
        });

        return base.CreateHost(builder);
    }
}
