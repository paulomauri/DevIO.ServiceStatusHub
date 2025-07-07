using MediatR;

namespace ServiceStatusHub.Application.Commands.Incident;

public record UpdateIncidentCommand(Guid incidentId, Guid serviceId, string Status) : IRequest;