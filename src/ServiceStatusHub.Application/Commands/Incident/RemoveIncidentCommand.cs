using MediatR;

namespace ServiceStatusHub.Application.Commands.Incident;

public record RemoveIncidentCommand(Guid IncidentId) : IRequest;