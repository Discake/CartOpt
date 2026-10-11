using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace CartOpt.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("PostgreSql")
            ?? throw new InvalidOperationException(
                "PostgreSQL connection string is not configured.");

        services.AddSingleton(
            _ => NpgsqlDataSource.Create(connectionString));

        return services;
    }
}
