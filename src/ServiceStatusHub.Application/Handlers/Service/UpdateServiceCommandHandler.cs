using MediatR;
using Microsoft.Extensions.Logging;
using ServiceStatusHub.Application.Commands.Service;
using ServiceStatusHub.Domain.Interfaces;

namespace ServiceStatusHub.Application.Handlers.Service;

public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly ILogger<UpdateServiceCommand> _logger;
    public UpdateServiceCommandHandler(IServiceRepository serviceRepository, ILogger<UpdateServiceCommand> logger)
    {
        _serviceRepository = serviceRepository;
        _logger = logger;
    }
    public async Task Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetByIdAsync(request.serviceId);

        if (service == null)
        {
            throw new KeyNotFoundException("Serviço não encontrado.");
        }

        await _serviceRepository.AddAsync(service);

        _logger.LogInformation("Service updated with ID: {ServiceId}, Name: {ServiceName}", service.Id, service.Name);

        return;
    }
}