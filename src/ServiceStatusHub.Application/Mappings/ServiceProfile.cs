using AutoMapper;
using ServiceStatusHub.Application.DTOs;
using ServiceStatusHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStatusHub.Application.Mappings
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ServiceDto>()
                .ForMember(dest => dest.serviceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.url, opt => opt.MapFrom(src => src.Url));
        }
    }
}
