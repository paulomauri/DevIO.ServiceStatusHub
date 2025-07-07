using MediatR;

namespace ServiceStatusHub.Application.Commands.Service;

public record RemoveServiceCommand(Guid serviceId) : IRequest;