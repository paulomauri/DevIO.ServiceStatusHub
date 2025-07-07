using MediatR;
using ServiceStatusHub.Application.DTOs;
using ServiceStatusHub.Application.Queries;
using ServiceStatusHub.Domain.Interfaces;

namespace ServiceStatusHub.Application.Handlers.Incident;

public class GetIncidentByIdQueryHandler : IRequestHandler<GetIncidentByIdQuery, IncidentDto>
{
    private readonly IIncidentRepository _repository;

    public GetIncidentByIdQueryHandler(IIncidentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IncidentDto> Handle(GetIncidentByIdQuery request, CancellationToken cancellationToken)
    {
        var incident = await _repository.GetByIdAsync(request.Id);

        return incident is null
            ? throw new KeyNotFoundException("Incident not found")
            : new IncidentDto(incident.Id, incident.ServiceId, incident.Status, incident.StartedAt, incident.ResolvedAt);
    }
}

