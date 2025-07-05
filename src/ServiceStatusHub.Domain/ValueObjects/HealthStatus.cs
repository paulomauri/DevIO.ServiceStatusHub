namespace ServiceStatusHub.Domain.ValueObjects;

public class HealthStatus
{
    public string Status { get; private set; } // OK, Warning, Critical
    public DateTime CheckedAt { get; private set; }

    public HealthStatus(string status)
    {
        Status = status;
        CheckedAt = DateTime.UtcNow;
    }

    public void Update(string status)
    {
        Status = status;
        CheckedAt = DateTime.UtcNow;
    }
}

