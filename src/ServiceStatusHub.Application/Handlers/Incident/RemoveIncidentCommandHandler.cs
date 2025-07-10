
using MediatR;
using Microsoft.Extensions.Logging;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Application.Interfaces;
using ServiceStatusHub.Domain.Interfaces;

namespace ServiceStatusHub.Application.Handlers.Incident;

public class RemoveIncidentCommandHandler : IRequestHandler<RemoveIncidentCommand>
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly ILogger<RemoveIncidentCommandHandler> _logger;
    private readonly IRedisCacheService _cache;
    private const string CacheKey = "incidents";
    public RemoveIncidentCommandHandler(IIncidentRepository incidentRepository, ILogger<RemoveIncidentCommandHandler> logger, IRedisCacheService cache)
    {
        _incidentRepository = incidentRepository;
        _logger = logger;
        _cache = cache;
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

        // Limpa o cache para garantir que os dados estejam atualizados
        await _cache.RemoveAsync(CacheKey);

        return; // Incident removed successfully
    }
}