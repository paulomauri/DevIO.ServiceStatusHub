using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStatusHub.Domain.Interfaces;
using ServiceStatusHub.Infrastructure.Configuration;
using ServiceStatusHub.Infrastructure.Context;
using ServiceStatusHub.Infrastructure.Repositories;

namespace ServiceStatusHub.Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        services.AddSingleton<IMongoDbContext, MongoDbContext>();

        services.AddScoped<IIncidentRepository, IncidentRepository>();
        services.AddScoped<IIncidentHistoryRepository, IncidentHistoryRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();

        return services;
    }
}
