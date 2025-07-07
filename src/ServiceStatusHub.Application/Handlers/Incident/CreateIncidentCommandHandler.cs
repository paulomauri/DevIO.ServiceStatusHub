using MediatR;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Domain.Entities;
using ServiceStatusHub.Domain.Interfaces;

public class CreateIncidentCommandHandler : IRequestHandler<CreateIncidentCommand, Guid>
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateIncidentCommandHandler(IIncidentRepository incidentRepository, IUnitOfWork unitOfWork)
    {
        _incidentRepository = incidentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
    {
        var incident = new Incident(request.ServiceId, request.description, request.Status);
        
        await _incidentRepository.AddAsync(incident);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return incident.Id;
    }
}
