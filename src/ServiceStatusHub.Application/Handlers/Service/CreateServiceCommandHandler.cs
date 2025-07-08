using MediatR;
using Microsoft.Extensions.Logging;
using ServiceStatusHub.Application.Commands.Service;
using ServiceStatusHub.Domain.Interfaces;

namespace ServiceStatusHub.Application.Handlers.Service;

public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Guid>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly ILogger<CreateServiceCommand> _logger;
    public CreateServiceCommandHandler(IServiceRepository serviceRepository, ILogger<CreateServiceCommand> logger)
    {
        _serviceRepository = serviceRepository;
        _logger = logger;
    }
    public async Task<Guid> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = new Domain.Entities.Service(request.name, request.url, (Domain.Enums.ServiceType) request.type, request.enviroment);
        
        await _serviceRepository.AddAsync(service);

        _logger.LogInformation("Service created with ID: {ServiceId}, Name: {ServiceName}", service.Id, service.Name);

        return service.Id;
    }
}
