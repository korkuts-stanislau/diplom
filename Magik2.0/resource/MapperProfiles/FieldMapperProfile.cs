using AutoMapper;
using Resource.UIModels;

namespace Resource.MapperProfiles;

public class FieldMapperProfile : Profile {
    public FieldMapperProfile()
    {
        CreateMap<Models.Field, FieldUI>()
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