using MediatR;
using ServiceStatusHub.Application.Commands.Service;
using ServiceStatusHub.Domain.Interfaces;

namespace ServiceStatusHub.Application.Handlers.Service;

public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateServiceCommandHandler(IServiceRepository serviceRepository, IUnitOfWork unitOfWork)
    {
        _serviceRepository = serviceRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetByIdAsync(request.serviceId);

        if (service == null)
        {
            throw new KeyNotFoundException("Serviço não encontrado.");
        }

        await _serviceRepository.AddAsync(service);

        await _unitOfWork.CommitAsync(cancellationToken);

        return;
    }
}