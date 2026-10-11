using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace CartOpt.Infrastructure.HealthChecks;

public sealed class PostgreSqlHealthCheck(
    NpgsqlDataSource dataSource) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await using var command =
                dataSource.CreateCommand("SELECT 1;");

            await command.ExecuteScalarAsync(cancellationToken);

            return HealthCheckResult.Healthy(
                "PostgreSQL доступен.");
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception)
        {
            return HealthCheckResult.Unhealthy(
                "Не удалось подключиться к PostgreSQL.",
                exception);
        }
    }
}
