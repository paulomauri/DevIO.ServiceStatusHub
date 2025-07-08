using MediatR;
using ServiceStatusHub.Application.DTOs;
using ServiceStatusHub.Application.Queries;
using ServiceStatusHub.Domain.Interfaces;


namespace ServiceStatusHub.Application.Handlers.Service
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, ServiceDto>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetServiceByIdQueryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<ServiceDto> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var service =  await _serviceRepository.GetByIdAsync(request.Id);

            if (service is null)
            {
                throw new KeyNotFoundException("Service not found");
            }

            return new ServiceDto(service.Id, service.Name, service.Url, (int) service.Type, service.Environment);
        }
    }
}
