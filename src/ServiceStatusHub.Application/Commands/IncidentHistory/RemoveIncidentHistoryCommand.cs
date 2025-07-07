using MediatR;

namespace ServiceStatusHub.Application.Commands.IncidentHistory;

public record RemoveIncidentHistoryCommand(Guid IncidentHistoryId) : IRequest;