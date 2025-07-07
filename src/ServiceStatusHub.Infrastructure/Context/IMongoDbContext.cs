using MongoDB.Driver;

namespace ServiceStatusHub.Infrastructure.Context;

public interface IMongoDbContext
{
    IMongoCollection<T> GetCollection<T>(string name);
}
