
using MediatR;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Domain.Interfaces;

namespace ServiceStatusHub.Application.Handlers.Incident;

public class RemoveIncidentCommandHandler : IRequestHandler<RemoveIncidentCommand>
{
    private readonly IIncidentRepository _incidentRepository;
    public RemoveIncidentCommandHandler(IIncidentRepository incidentRepository)
    {
        _incidentRepository = incidentRepository;
    }
    public async Task Handle(RemoveIncidentCommand request, CancellationToken cancellationToken)
    {
        var incident = await _incidentRepository.GetByIdAsync(request.IncidentId);

        if (incident == null)
        {
            throw new KeyNotFoundException("Incidente não encontrado.");
        }

        await _incidentRepository.DeleteAsync(incident);

        return; // Incident removed successfully
    }
}