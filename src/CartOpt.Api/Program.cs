using CartOpt.Infrastructure;
using CartOpt.Infrastructure.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services
    .AddHealthChecks()
    .AddCheck<PostgreSqlHealthCheck>(
        "postgresql",
        tags: ["ready"]);

var app = builder.Build();

app.MapHealthChecks(
    "/health/live",
    new HealthCheckOptions
    {
        Predicate = _ => false
    });

app.MapHealthChecks(
    "/health/ready",
    new HealthCheckOptions
    {
        Predicate = check =>
            check.Tags.Contains("ready")
    });

app.Run();

public partial class Program;
