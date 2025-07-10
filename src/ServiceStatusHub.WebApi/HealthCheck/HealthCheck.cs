using MongoDB.Driver;
using StackExchange.Redis;
using ServiceStatusHub.WebApi.HealthCheck.MemoryHealthCheck;
using ServiceStatusHub.WebApi.HealthCheck.RemoteHealthCheck;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ServiceStatusHub.WebApi.HealthCheck;

public static class HealthCheck
{
    /// <summary>
    /// Configures health checks for the application.
    /// </summary>
    /// <param name="services">The service collection to add health checks to.</param>
    /// <param name="configuration">The configuration containing connection strings and other settings.</param>
    /// <remarks>
    /// This method adds a SQL Server health check and configures the Health Checks UI.
    /// </remarks>
    public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddMongoDb(sp => new MongoClient(configuration["ConnectionStrings:MongoDb"]),
                        name: "mongodb",
                        tags: new string[] { "db", "data" })
            .AddRedis(ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")),
                        name: "redis",
                        tags: new string[] { "cache", "data" })
            .AddCheck<ServiceStatusHub.WebApi.HealthCheck.RemoteHealthCheck.RemoteHealthCheck>("Remote endpoints Health Check", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Remote Health Check " })
            .AddCheck<ServiceStatusHub.WebApi.HealthCheck.MemoryHealthCheck.MemoryHealthCheck>($"Feedback Service Memory Check", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Memory Health Check " });

        services.AddHealthChecksUI(opt =>
        {
            opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
            opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
            opt.SetApiMaxActiveRequests(1); //api requests concurrency    
            opt.AddHealthCheckEndpoint("feedback api", "/api/health"); //map health check api    

        })
            .AddInMemoryStorage();
    }
}
