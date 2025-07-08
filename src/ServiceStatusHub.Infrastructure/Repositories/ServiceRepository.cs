using MongoDB.Driver;
using ServiceStatusHub.Domain.Entities;
using ServiceStatusHub.Domain.Interfaces;
using ServiceStatusHub.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStatusHub.Infrastructure.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly IMongoCollection<Service> _collection;

    public ServiceRepository(IMongoDbContext context)
    {
        _collection = context.GetCollection<Service>("service");
    }

    public async Task AddAsync(Service service)
    {
        await _collection.InsertOneAsync(service);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Service>> GetAllAsync()
    {
        return await _collection
            .Find(_ => true)
            .SortBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Service?> GetByIdAsync(Guid id)
    {
        return await _collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public Task UpdateAsync(Service service)
    {
        var filter = Builders<Service>.Filter.Eq(x => x.Id, service.Id);
        return _collection.ReplaceOneAsync(filter, service);
    }

}
