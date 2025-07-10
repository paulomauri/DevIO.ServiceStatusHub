using MediatR;
using ServiceStatusHub.Application.DTOs;
using ServiceStatusHub.Application.Interfaces;
using ServiceStatusHub.Application.Queries;
using ServiceStatusHub.Domain.Interfaces;

namespace ServiceStatusHub.Application.Handlers.Incident;

public class GetIncidentByIdQueryHandler : IRequestHandler<GetIncidentByIdQuery, IncidentDto>
{
    private readonly IIncidentRepository _repository;
    private readonly IRedisCacheService _cache;
    private readonly ICachePolicyService _policy;
    private const string CacheKey = "incident";

    public GetIncidentByIdQueryHandler(IIncidentRepository repository, IRedisCacheService cache, ICachePolicyService policy)
    {
        _repository = repository;
        _cache = cache;
        _policy = policy;
    }

    public async Task<IncidentDto> Handle(GetIncidentByIdQuery request, CancellationToken cancellationToken)
    {
        var cached = await _cache.GetAsync<IncidentDto>($"{CacheKey}_{request.Id}");
        if (cached != null)
            return cached;

        var incident = await _repository.GetByIdAsync(request.Id);

        var ttl = _policy.GetExpirationFor(CacheKey);

        if (incident is null)
        {
            // If the incident is not found, cache a null value to avoid repeated database calls
            throw new KeyNotFoundException("Incident not found");
        }
        // Cache the product details
        await _cache.SetAsync($"{CacheKey}_{request.Id}",
            new IncidentDto
            (
                incident.Id,
                incident.ServiceId,
                incident.Status,
                incident.StartedAt,
                incident.ResolvedAt
            ),
            ttl);

        return incident is null
            ? throw new KeyNotFoundException("Incident not found")
            : new IncidentDto(incident.Id, incident.ServiceId, incident.Status, incident.StartedAt, incident.ResolvedAt);
    }
}

