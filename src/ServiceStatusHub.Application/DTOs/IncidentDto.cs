
namespace ServiceStatusHub.Application.DTOs;

public record IncidentDto(Guid id, Guid serviceId, string status, DateTime startedAt, DateTime? resolvedAt);