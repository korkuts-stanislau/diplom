using AutoMapper;
using Resource.UIModels;

namespace Resource.MapperProfiles;

public class ProjectAreaMapperProfile : Profile {
    public ProjectAreaMapperProfile()
    {
        CreateMap<Models.ProjectArea, ProjectAreaUI>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id)
            )
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name)
            )
            .ForMember(
                dest => dest.Icon,
                opt => opt.MapFrom(src => src.Icon != null ? Convert.ToBase64String(src.Icon) : "")
            );
    }
}