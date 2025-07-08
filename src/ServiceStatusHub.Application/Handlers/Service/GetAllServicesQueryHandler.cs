using MediatR;
using ServiceStatusHub.Application.DTOs;
using ServiceStatusHub.Application.Queries;
using ServiceStatusHub.Domain.Interfaces;


namespace ServiceStatusHub.Application.Handlers.Service
{
    public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, IEnumerable<ServiceDto>>
    {
        private readonly IServiceRepository _serviceRepository;
        
        public GetAllServicesQueryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<IEnumerable<ServiceDto>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            var services = await _serviceRepository.GetAllAsync();
            return services.Select(service => new ServiceDto(service.Id, service.Name, service.Url, (int)service.Type, service.Environment));
        }
    }
}