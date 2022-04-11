using AutoMapper;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.MapperProfiles;

public class StageMapperProfile : Profile {
    public StageMapperProfile()
    {
        CreateMap<Models.Stage, StageUI>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id)
            )
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name)
            )
            .ForMember(
                dest => dest.Description,
                opt => opt.MapFrom(src => src.Description)
            )
            .ForMember(
                dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate)
            )
            .ForMember(
                dest => dest.Deadline,
                opt => opt.MapFrom(src => src.Deadline)
            )
            .ForMember(
                dest => dest.Progress,
                opt => opt.MapFrom(src => src.Progress)
            )
            .ForMember(
                dest => dest.Color,
                opt => opt.MapFrom(src => ColorEvaluator.GetStageColor(src))
            );
    }
}