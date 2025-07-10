using MediatR;
using ServiceStatusHub.Application.DTOs;
using ServiceStatusHub.Application.Interfaces;
using ServiceStatusHub.Application.Queries;
using ServiceStatusHub.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStatusHub.Application.Handlers.Incident
{
    public class GetAllIncidentsQueryHandler : IRequestHandler<GetAllIncidentsQuery, IEnumerable<IncidentDto>>
    {
        private readonly IIncidentRepository _repository;
        private readonly IRedisCacheService _cache;
        private readonly ICachePolicyService _policy;
        private const string CacheKey = "incidents";
    
        public GetAllIncidentsQueryHandler(IIncidentRepository repository, IRedisCacheService cache, ICachePolicyService policy)
        {
            _repository = repository;
            _cache = cache;
            _policy = policy;
        }

        public async Task<IEnumerable<IncidentDto>> Handle(GetAllIncidentsQuery request, CancellationToken cancellationToken)
        { 
            var cache = _cache.GetAsync<List<IncidentHistoryDto>>($"{CacheKey}");

            var list = await _repository.GetAllAsync();
         
            if (list == null || !list.Any())
            {
                return new List<IncidentDto>();
            }

            var ttl = _policy.GetExpirationFor(CacheKey);

            // Cache the product details
            await _cache.SetAsync($"{CacheKey}", list, ttl);

            var incidentDtos = list.Select(incident => new IncidentDto(
                incident.Id,
                incident.ServiceId,
                incident.Description ?? "",
                incident.Status.ToString(),
                incident.StartedAt,
                incident.ResolvedAt));

            return incidentDtos;
        }
    }
}
