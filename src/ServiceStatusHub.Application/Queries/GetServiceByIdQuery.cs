using MediatR;
using ServiceStatusHub.Application.DTOs;

namespace ServiceStatusHub.Application.Queries;

public record GetServiceByIdQuery(Guid Id) : IRequest<ServiceDto>;
