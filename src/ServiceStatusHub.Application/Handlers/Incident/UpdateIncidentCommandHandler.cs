using MediatR;
using Microsoft.Extensions.Logging;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Application.Interfaces;
using ServiceStatusHub.Domain.Entities;
using ServiceStatusHub.Domain.Interfaces;

public class UpdateIncidentCommandHandler : IRequestHandler<UpdateIncidentCommand>
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly ILogger<UpdateIncidentCommandHandler> _logger;
    private readonly IRedisCacheService _cache;
    private const string CacheKey = "incidents";

    public UpdateIncidentCommandHandler(IIncidentRepository incidentRepository, ILogger<UpdateIncidentCommandHandler> logger, IRedisCacheService cache)
    {
        _incidentRepository = incidentRepository;
        _logger = logger;
        _cache = cache;
    }

    public async Task Handle(UpdateIncidentCommand request, CancellationToken cancellationToken)
    {
        var incident = await _incidentRepository.GetByIdAsync(request.incidentId);

        if (incident == null)
        {
            throw new KeyNotFoundException("Incidente não encontrado.");
        }

        await _incidentRepository.UpdateAsync(incident);

        _logger.LogInformation("Incident with ID {IncidentId} for Service: {ServiceId} with Status: {Status} - updated",
        incident.Id, incident.ServiceId, incident.Status);

        await _cache.RemoveAsync(CacheKey);
    }
}