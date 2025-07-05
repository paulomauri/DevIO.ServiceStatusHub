using ServiceStatusHub.Domain.Enums;

namespace ServiceStatusHub.Domain.Entities;

public class IncidentHistory
{
    public Guid Id { get; private set; }
    public Guid IncidentId { get; private set; }
    public IncidentAction Action { get; private set; }       // e.g., "Created", "StatusChanged", "Resolved", "AttachmentAdded"
    public string? Description { get; private set; } // texto livre, comentário ou detalhe
    public string? PerformedBy { get; private set; } // usuário, sistema, etc.
    public DateTime Timestamp { get; private set; }

    public IncidentHistory(Guid incidentId, IncidentAction action, string? description, string? performedBy)
    {
        Id = Guid.NewGuid();
        IncidentId = incidentId;
        Action = action;
        Description = description;
        PerformedBy = performedBy;
        Timestamp = DateTime.UtcNow;
    }

