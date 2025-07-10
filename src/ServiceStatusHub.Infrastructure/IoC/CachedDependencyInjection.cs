using Microsoft.Extensions.DependencyInjection;
using ServiceStatusHub.Application.Interfaces;
using ServiceStatusHub.Infrastructure.ExternalServices.Cache;
using ServiceStatusHub.Infrastructure.ExternalServices.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStatusHub.Infrastructure.IoC
{
    public static class CachedDependencyInjection
    {
        public static IServiceCollection AddCachedDependencies(this IServiceCollection services)
        {
            // Register Redis cache service
            services.AddSingleton<IRedisCacheService, RedisCacheService>();
            services.AddSingleton<ICachePolicyService, CachePolicyService>();

            return services;
        }
    }
}
