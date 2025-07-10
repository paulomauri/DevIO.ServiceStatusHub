using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using ServiceStatusHub.Domain.Entities;
using ServiceStatusHub.Domain.Interfaces;
using ServiceStatusHub.Infrastructure.Configuration;
using ServiceStatusHub.Infrastructure.Context;
using ServiceStatusHub.Infrastructure.Repositories;

namespace ServiceStatusHub.Infrastructure.IoC
{
    public static class InfrastructureConfigurationMapping
    {
        public static IServiceCollection AddInfrastructureConfigurationMapping(this IServiceCollection services)
        {
            BsonClassMap.RegisterClassMap<Incident>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id)
                  .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
                cm.MapProperty(c => c.ServiceId)
                  .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
            });

            BsonClassMap.RegisterClassMap<IncidentHistory>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id)
                  .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
                cm.MapProperty(c => c.IncidentId)
                  .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
            });

            BsonClassMap.RegisterClassMap<Service>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id)
                  .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
            });

            return services;
        }
    }
}
