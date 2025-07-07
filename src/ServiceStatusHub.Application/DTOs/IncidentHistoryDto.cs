
namespace ServiceStatusHub.Application.DTOs;

public record IncidentHistoryDto(Guid Id, Guid IncidentId, string Action, string? Description, string? PerformedBy, DateTime Timestamp);
