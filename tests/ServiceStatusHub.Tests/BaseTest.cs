using ServiceStatusHub.Application.Interfaces;
using ServiceStatusHub.Domain.Entities;
using ServiceStatusHub.Infrastructure.Context;
using ServiceStatusHub.WebApi;
using System.Reflection;

namespace ServiceStatusHub.Tests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Incident).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(IRedisCacheService).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(MongoDbContext).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}