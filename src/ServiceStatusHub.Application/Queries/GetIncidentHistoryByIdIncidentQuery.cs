using MediatR;
using ServiceStatusHub.Application.DTOs;

namespace ServiceStatusHub.Application.Queries;

public record GetIncidentHistoryByIdIncidentQuery(Guid idIncident) : IRequest<List<IncidentHistoryDto>>;
