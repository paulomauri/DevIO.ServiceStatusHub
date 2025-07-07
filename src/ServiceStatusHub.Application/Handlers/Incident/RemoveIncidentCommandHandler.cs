
using MediatR;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Domain.Interfaces;

namespace ServiceStatusHub.Application.Handlers.Incident;

public class RemoveIncidentCommandHandler : IRequestHandler<RemoveIncidentCommand>
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RemoveIncidentCommandHandler(IIncidentRepository incidentRepository, IUnitOfWork unitOfWork)
    {
        _incidentRepository = incidentRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(RemoveIncidentCommand request, CancellationToken cancellationToken)
    {
        var incident = await _incidentRepository.GetByIdAsync(request.IncidentId);

        if (incident == null)
        {
            throw new KeyNotFoundException("Incidente não encontrado.");
        }

        await _incidentRepository.DeleteAsync(incident);

        await _unitOfWork.CommitAsync(cancellationToken);

        return; // Incident removed successfully
    }
}