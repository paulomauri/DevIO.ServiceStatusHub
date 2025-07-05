
namespace ServiceStatusHub.Domain.Enums;

public enum IncidentAction
{
    Created,        // Incident created
    StatusChanged,  // Status changed
    Resolved,       // Incident resolved 
    Reopened,       // Incident reopened
    CommentAdded,   // Comment added
    AttachmentAdded,    // Attachment added
    Escalated,      // Incident escalated
    Acknowledged,   // Incident acknowledged
    Closed          // Incident closed
}
