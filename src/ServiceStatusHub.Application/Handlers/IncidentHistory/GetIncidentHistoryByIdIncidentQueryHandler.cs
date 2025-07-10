using MediatR;
using ServiceStatusHub.Application.DTOs;
using ServiceStatusHub.Application.Interfaces;
using ServiceStatusHub.Application.Queries;
using ServiceStatusHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStatusHub.Application.Handlers.IncidentHistory
{
    public class GetIncidentHistoryByIdIncidentQueryHandler : IRequestHandler<GetIncidentHistoryByIdIncidentQuery, List<IncidentHistoryDto>>
    {
        private readonly IIncidentHistoryRepository _repository;
        private readonly IRedisCacheService _cache;
        private readonly ICachePolicyService _policy;
        private const string CacheKey = "incidentHistory";

        public GetIncidentHistoryByIdIncidentQueryHandler(IIncidentHistoryRepository repository, IRedisCacheService cache, ICachePolicyService policy)
        {
            _repository = repository;
            _cache = cache;
            _policy = policy;
        }

        public async Task<List<IncidentHistoryDto>> Handle(GetIncidentHistoryByIdIncidentQuery request, CancellationToken cancellationToken)
        {
            var cache = _cache.GetAsync<List<IncidentHistoryDto>>($"{CacheKey}_{request.idIncident}");

            var incidentHistories = await _repository.GetRecentAsync(request.idIncident);
         
            if (incidentHistories == null || !incidentHistories.Any())
            {
                throw new KeyNotFoundException("No incident history found for the given incident ID.");
            }

            var ttl = _policy.GetExpirationFor(CacheKey);

            var lista = incidentHistories.Select(ih => new IncidentHistoryDto(ih.Id, ih.IncidentId, ih.Action.ToString(), ih.Description, ih.PerformedBy, ih.Timestamp)).ToList();

            // Cache the product details
            await _cache.SetAsync($"{CacheKey}_{request.idIncident}",
                lista,
                ttl);

            return lista;
        }
    }
}
