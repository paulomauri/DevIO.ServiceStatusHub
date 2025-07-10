
namespace ServiceStatusHub.Application.DTOs;

public record IncidentDto(Guid id, Guid serviceId, string description, string status, DateTime startedAt, DateTime? resolvedAt);