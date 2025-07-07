using MediatR;

namespace ServiceStatusHub.Application.Commands.Service;

public record CreateServiceCommand(string name, string url, int type, string enviroment) : IRequest<Guid>;