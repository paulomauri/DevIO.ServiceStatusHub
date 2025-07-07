using MediatR;
using ServiceStatusHub.Application.Commands.Service;
using ServiceStatusHub.Domain.Interfaces;

namespace ServiceStatusHub.Application.Handlers.Service;

public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Guid>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateServiceCommandHandler(IServiceRepository serviceRepository, IUnitOfWork unitOfWork)
    {
        _serviceRepository = serviceRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Guid> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = new Domain.Entities.Service(request.name, request.url, (Domain.Enums.ServiceType) request.type, request.enviroment);
        
        await _serviceRepository.AddAsync(service);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return service.Id;
    }
}
