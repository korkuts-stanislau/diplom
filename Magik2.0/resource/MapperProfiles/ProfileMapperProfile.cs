using AutoMapper;
using Resource.UIModels;

namespace Resource.MapperProfiles;

public class ProfileMapperProfile : Profile {
    public ProfileMapperProfile()
    {
        CreateMap<Models.Profile, ProfileUI>()
            .ForMember(
                dest => dest.Username,
                opt => opt.MapFrom(src => src.Username)
            )
            .ForMember(
                dest => dest.Description,
                opt => opt.MapFrom(src => src.Description)
            )
            .ForMember(
                dest => dest.Picture,
                opt => opt.MapFrom(src => src.Picture != null ? Convert.ToBase64String(src.Picture) : "")
            );
    }
}