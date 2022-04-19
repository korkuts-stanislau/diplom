using System.Drawing;
using AutoMapper;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.MapperProfiles;

public class ProjectMapperProfile : Profile {
    public ProjectMapperProfile()
    {
        CreateMap<Models.Project, ProjectUI>()
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
                dest => dest.Color,
                opt => opt.MapFrom(src => ColorEvaluator.GetProjectColor(src))
            );
    }
}