using AutoMapper;
using ServiceStatusHub.Application.Commands.Incident;
using ServiceStatusHub.Application.Commands.IncidentHistory;
using ServiceStatusHub.Application.DTOs;
using ServiceStatusHub.Domain.Entities;

namespace ServiceStatusHub.Application.Mappings;

public class IncidentHistoryProfile : Profile
{
    public IncidentHistoryProfile()
    {
        CreateMap<IncidentHistory, IncidentHistoryDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.IncidentId, opt => opt.MapFrom(src => src.IncidentId))
            .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action.ToString()))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.PerformedBy, opt => opt.MapFrom(src => src.PerformedBy))
            .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp));

        CreateMap<IncidentHistoryDto, IncidentHistory>();

        CreateMap<IncidentHistory, AddIncidentHistoryCommand>();
    }
}
