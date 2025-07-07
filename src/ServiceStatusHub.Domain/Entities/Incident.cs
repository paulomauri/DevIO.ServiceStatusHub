using ServiceStatusHub.Domain.ValueObjects;

namespace ServiceStatusHub.Domain.Entities;

public class Incident
{
    public Guid Id { get; private set; }
    public Guid ServiceId { get; private set; }
    public string? Description { get; set; }
    public string Status { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime? ResolvedAt { get; private set; }

    private readonly List<Attachment> _attachments = new();
    public IReadOnlyCollection<Attachment> Attachments => _attachments;

    public Incident(Guid serviceId, string description, string status)
    {
        Id = Guid.NewGuid();
        ServiceId = serviceId;
        Status = status;
        Description = description;
        StartedAt = DateTime.UtcNow;
    }

    public void Resolve()
    {
        ResolvedAt = DateTime.UtcNow;
    }

    public void AddAttachment(Attachment attachment)
    {
        _attachments.Add(attachment);
    }
}
