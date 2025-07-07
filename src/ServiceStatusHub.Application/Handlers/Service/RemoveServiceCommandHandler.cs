
using MediatR;
using ServiceStatusHub.Application.Commands.Service;
using ServiceStatusHub.Domain.Interfaces;

namespace ServiceStatusHub.Application.Handlers.Service;

public class RemoveServiceCommandHandler : IRequestHandler<RemoveServiceCommand>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RemoveServiceCommandHandler(IServiceRepository serviceRepository, IUnitOfWork unitOfWork)
    {
        _serviceRepository = serviceRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(RemoveServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetByIdAsync(request.serviceId);
 
        if (service == null)
        {
            throw new KeyNotFoundException("Serviço não encontrado.");
        }
        
        await _serviceRepository.DeleteAsync(service.Id);
 
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}