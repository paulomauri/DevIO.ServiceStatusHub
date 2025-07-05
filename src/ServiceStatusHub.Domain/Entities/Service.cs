using ServiceStatusHub.Domain.Enums;
using ServiceStatusHub.Domain.ValueObjects;

namespace ServiceStatusHub.Domain.Entities;

public class Service
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Url { get; private set; }
    public ServiceType Type { get; private set; }
    public string Environment { get; private set; }

    public HealthStatus LastStatus { get; private set; }

    public Service(string name, string url, ServiceType type, string environment)
    {
        Id = Guid.NewGuid();
        Name = name;
        Url = url;
        Type = type;
        Environment = environment;
        LastStatus = new HealthStatus("Unknown");
    }

    public void UpdateStatus(string status)
    {
        LastStatus.Update(status);
    }

    public void UpdateInfo(string name, string url, ServiceType type, string environment)
    {
        Name = name;
        Url = url;
        Type = type;
        Environment = environment;
    }
}
