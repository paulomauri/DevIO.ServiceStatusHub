using ServiceStatusHub.Domain.Enums;

namespace ServiceStatusHub.WebApi.Models
{
    public class AddIncidentHistoryRequest
    {
        public Guid IncidentId { get; set; }
        public IncidentAction Action { get; set; }
        public string? Description { get; set; }
        public string? PerformedBy { get; set; }
    }
}
