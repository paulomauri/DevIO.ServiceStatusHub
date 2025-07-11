using MediatR;
using ServiceStatusHub.Application.DTOs;
using ServiceStatusHub.Application.Interfaces;
using ServiceStatusHub.Application.Queries;
using ServiceStatusHub.Domain.Interfaces;


namespace ServiceStatusHub.Application.Handlers.Incident
{
    public class GetRecentIncidentQueryHandler : IRequestHandler<GetRecentIncidentQuery, List<IncidentDto>>
    {
        private readonly IIncidentRepository _repository;
        private readonly IRedisCacheService _cache;
        private readonly ICachePolicyService _policy;
        private const string CacheKey = "incidents";

        public GetRecentIncidentQueryHandler(IIncidentRepository repository, IRedisCacheService cache, ICachePolicyService policy)
        {
            _repository = repository;
            _cache = cache;
            _policy = policy;
        }

        public async Task<List<IncidentDto>> Handle(GetRecentIncidentQuery request, CancellationToken cancellationToken)
        {
            var incidents = await _repository.GetRecentAsync(request.count);

            if (incidents == null || !incidents.Any())
            {
                throw new KeyNotFoundException("No incident found.");
            }

            var lista = incidents.Select(ih => new IncidentDto(ih.Id, ih.ServiceId, ih.Description, ih.Status, ih.StartedAt, ih.ResolvedAt)).ToList();

            return lista;
        }
    }
}
