using MongoDB.Driver;
using ServiceStatusHub.Domain.Entities;
using ServiceStatusHub.Domain.Interfaces;
using ServiceStatusHub.Infrastructure.Context;

namespace ServiceStatusHub.Infrastructure.Repositories;

public class IncidentRepository : IIncidentRepository
{
    private readonly IMongoCollection<Incident> _collection;

    public IncidentRepository(IMongoDbContext context)
    {
        _collection = context.GetCollection<Incident>("incidents");
    }

    public async Task AddAsync(Incident incident)
    {
        await _collection.InsertOneAsync(incident);
    }

    public async Task DeleteAsync(Incident incident)
    {
        await _collection.DeleteOneAsync(x => x.Id == incident.Id);
    }

    public async Task<IEnumerable<Incident>> GetAllAsync()
    {
        return await _collection
            .Find(_ => true)
            .SortByDescending(x => x.StartedAt)
            .ToListAsync();
    }

    public async Task<Incident?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Incident>> GetByServiceIdAsync(Guid serviceId)
    {
        return await _collection
            .Find(x => x.ServiceId == serviceId)
            .SortByDescending(x => x.StartedAt)
            .ToListAsync();
    }

    public Task ResolveAsync(Guid incidentId)
    {
        return _collection.UpdateOneAsync(
            x => x.Id == incidentId,
            Builders<Incident>.Update.Set(x => x.ResolvedAt, DateTime.UtcNow)
        );
    }

    public async Task UpdateAsync(Incident incident)
    {
        var filter = Builders<Incident>.Filter.Eq(x => x.Id, incident.Id);
        await _collection.ReplaceOneAsync(filter, incident);
    }
}
