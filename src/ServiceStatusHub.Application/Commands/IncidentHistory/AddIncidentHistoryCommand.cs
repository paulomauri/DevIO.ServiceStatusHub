using MediatR;
using ServiceStatusHub.Domain.Enums;

namespace ServiceStatusHub.Application.Commands.IncidentHistory;

public record AddIncidentHistoryCommand(Guid IncidentId, IncidentAction Action, string? Description, string? PerformedBy) : IRequest<Guid>;