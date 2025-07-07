using MediatR;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Domain.Entities;
using ServiceStatusHub.Domain.Interfaces;

public class CreateIncidentCommandHandler : IRequestHandler<CreateIncidentCommand, Guid>
{
    private readonly IIncidentRepository _incidentRepository;
    public CreateIncidentCommandHandler(IIncidentRepository incidentRepository)
    {
        _incidentRepository = incidentRepository;
    }

    public async Task<Guid> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
    {
        var incident = new Incident(request.ServiceId, request.description, request.Status);
        
        await _incidentRepository.AddAsync(incident);
        
        return incident.Id;
    }
}
