using MediatR;

namespace ServiceStatusHub.Application.Commands.Service;

public record UpdateServiceCommand(Guid serviceId, string name, string url, int type, string environment) : IRequest;