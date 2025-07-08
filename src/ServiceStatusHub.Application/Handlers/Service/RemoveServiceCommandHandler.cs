
using MediatR;
using Microsoft.Extensions.Logging;
using ServiceStatusHub.Application.Commands.Service;
using ServiceStatusHub.Domain.Interfaces;

namespace ServiceStatusHub.Application.Handlers.Service;

public class RemoveServiceCommandHandler : IRequestHandler<RemoveServiceCommand>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly ILogger<RemoveServiceCommand> _logger;
    public RemoveServiceCommandHandler(IServiceRepository serviceRepository, ILogger<RemoveServiceCommand> logger)
    {
        _serviceRepository = serviceRepository;
        _logger = logger;
    }
    public async Task Handle(RemoveServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetByIdAsync(request.serviceId);
 
        if (service == null)
        {
            throw new KeyNotFoundException("Serviço não encontrado.");
        }
        
        await _serviceRepository.DeleteAsync(service.Id);

        _logger.LogWarning("Removed service with ID: {ServiceId}, Name: {ServiceName}", service.Id, service.Name);
    }
}