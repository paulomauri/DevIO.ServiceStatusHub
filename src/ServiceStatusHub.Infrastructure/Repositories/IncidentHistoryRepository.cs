using MongoDB.Driver;
using ServiceStatusHub.Domain.Entities;
using ServiceStatusHub.Domain.Interfaces;
using ServiceStatusHub.Infrastructure.Context;

namespace ServiceStatusHub.Infrastructure.Repositories;

public class IncidentHistoryRepository : IIncidentHistoryRepository
{
    private readonly IMongoCollection<IncidentHistory> _collection;

    public IncidentHistoryRepository(IMongoDbContext context)
    {
        _collection = context.GetCollection<IncidentHistory>("incident_histories");
    }

    public async Task AddAsync(IncidentHistory history)
    {
        await _collection.InsertOneAsync(history);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }

    public async Task<IncidentHistory> GetByIdAsync(Guid id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<IncidentHistory>> GetByIncidentIdAsync(Guid incidentId)
    {
        return await _collection.Find(x => x.IncidentId == incidentId).ToListAsync();
    }

    public async Task<List<IncidentHistory>> GetRecentAsync(Guid incidentId, int count = 50)
    {
        return await _collection.Find(_ => true)
            .SortByDescending(x => x.Timestamp)
            .Limit(count)
            .ToListAsync();
    }
}
