using ServiceStatusHub.Domain.Entities;

namespace ServiceStatusHub.Domain.Interfaces;

public interface IIncidentRepository
{
    Task<Incident?> GetByIdAsync(Guid id);
    Task<IEnumerable<Incident>> GetAllAsync();
    Task<List<Incident>> GetByServiceIdAsync(Guid serviceId);
    Task AddAsync(Incident incident);
    Task UpdateAsync(Incident incident);
    Task ResolveAsync(Guid incidentId);
    Task DeleteAsync(Incident incident);
}
