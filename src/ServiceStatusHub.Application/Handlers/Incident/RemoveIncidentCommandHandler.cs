
using MediatR;
using Microsoft.Extensions.Logging;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Domain.Interfaces;

namespace ServiceStatusHub.Application.Handlers.Incident;

public class RemoveIncidentCommandHandler : IRequestHandler<RemoveIncidentCommand>
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly ILogger<RemoveIncidentCommandHandler> _logger;
    public RemoveIncidentCommandHandler(IIncidentRepository incidentRepository, ILogger<RemoveIncidentCommandHandler> logger)
    {
        _incidentRepository = incidentRepository;
        _logger = logger;
    }
    public async Task Handle(RemoveIncidentCommand request, CancellationToken cancellationToken)
    {
        var incident = await _incidentRepository.GetByIdAsync(request.IncidentId);

        if (incident == null)
        {
            throw new KeyNotFoundException("Incidente não encontrado.");
        }

        await _incidentRepository.DeleteAsync(incident);

        _logger.LogWarning("Incident with ID {IncidentId} for Service: {ServiceId} with Status: {Status} - removed",
            incident.Id, incident.ServiceId, incident.Status);

        return; // Incident removed successfully
    }
}