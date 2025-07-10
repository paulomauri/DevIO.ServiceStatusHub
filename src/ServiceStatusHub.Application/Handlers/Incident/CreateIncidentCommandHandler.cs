using MediatR;
using Microsoft.Extensions.Logging;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Application.Interfaces;
using ServiceStatusHub.Domain.Entities;
using ServiceStatusHub.Domain.Interfaces;

public class CreateIncidentCommandHandler : IRequestHandler<CreateIncidentCommand, Guid>
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly ILogger<CreateIncidentCommandHandler> _logger;
    private readonly IRedisCacheService _cache;
    private const string CacheKey = "incidents";

    public CreateIncidentCommandHandler(IIncidentRepository incidentRepository, ILogger<CreateIncidentCommandHandler> logger, IRedisCacheService cache)
    {
        _incidentRepository = incidentRepository;
        _logger = logger;
        _cache = cache;
    }

    public async Task<Guid> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
    {
        var incident = new Incident(request.ServiceId, request.description, request.Status);
        
        await _incidentRepository.AddAsync(incident);

        _logger.LogInformation(
            "Incident created: {IncidentId} for Service: {ServiceId} with Status: {Status}",
            incident.Id, request.ServiceId, request.Status);

        // Limpa o cache para garantir que os dados estejam atualizados
        await _cache.RemoveAsync(CacheKey);

        return incident.Id;
    }
}
