using AutoMapper;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Application.DTOs;
using ServiceStatusHub.Domain.Entities;

namespace ServiceStatusHub.Application.Mappings;

public class IncidentProfile : Profile
{
    public IncidentProfile()
    {
        //CreateMap<Incident, IncidentDto>()
        //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        //    .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
        //    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
        //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        //    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

        CreateMap<Incident, IncidentDto>();
        CreateMap<IncidentHistory, IncidentHistoryDto>();
        // Também poderia mapear comandos -> entidade se desejar
        CreateMap<CreateIncidentCommand, Incident>();
        CreateMap<UpdateIncidentCommand, Incident>();

    }
}