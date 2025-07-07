using MediatR;
using ServiceStatusHub.Application.DTOs;

namespace ServiceStatusHub.Application.Queries;

public record GetIncidentByIdQuery(Guid Id) : IRequest<IncidentDto>;
