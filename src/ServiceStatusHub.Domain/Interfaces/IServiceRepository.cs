using ServiceStatusHub.Domain.Entities;

namespace ServiceStatusHub.Domain.Interfaces;

public interface IServiceRepository
{
    Task<Service?> GetByIdAsync(Guid id);
    Task<List<Service>> GetAllAsync();
    Task AddAsync(Service service);
    Task UpdateAsync(Service service);
    Task DeleteAsync(Guid id);
}
