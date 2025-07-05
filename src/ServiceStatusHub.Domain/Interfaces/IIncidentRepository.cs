using ServiceStatusHub.Domain.Entities;

namespace ServiceStatusHub.Domain.Interfaces;

public interface IIncidentRepository
{
    Task<Incident?> GetByIdAsync(Guid id);
    Task<List<Incident>> GetByServiceIdAsync(Guid serviceId);
    Task AddAsync(Incident incident);
    Task ResolveAsync(Guid incidentId);
}
