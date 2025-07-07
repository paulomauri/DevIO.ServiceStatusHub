using MediatR;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Domain.Entities;
using ServiceStatusHub.Domain.Interfaces;

public class UpdateIncidentCommandHandler : IRequestHandler<UpdateIncidentCommand>
{
    private readonly IIncidentRepository _incidentRepository;

    public UpdateIncidentCommandHandler(IIncidentRepository incidentRepository)
    {
        _incidentRepository = incidentRepository;
    }

    public async Task Handle(UpdateIncidentCommand request, CancellationToken cancellationToken)
    {
        var incident = await _incidentRepository.GetByIdAsync(request.incidentId);

        if (incident == null)
        {
            throw new KeyNotFoundException("Incidente não encontrado.");
        }

        await _incidentRepository.UpdateAsync(incident);

    }
}