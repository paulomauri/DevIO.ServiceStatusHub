using MediatR;
using Microsoft.Extensions.Logging;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Domain.Entities;
using ServiceStatusHub.Domain.Interfaces;

public class CreateIncidentCommandHandler : IRequestHandler<CreateIncidentCommand, Guid>
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly ILogger<CreateIncidentCommandHandler> _logger;
    public CreateIncidentCommandHandler(IIncidentRepository incidentRepository, ILogger<CreateIncidentCommandHandler> logger)
    {
        _incidentRepository = incidentRepository;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
    {
        var incident = new Incident(request.ServiceId, request.description, request.Status);
        
        await _incidentRepository.AddAsync(incident);

        _logger.LogInformation(
            "Incident created: {IncidentId} for Service: {ServiceId} with Status: {Status}",
            incident.Id, request.ServiceId, request.Status);

        return incident.Id;
    }
}
