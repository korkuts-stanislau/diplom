using AutoMapper;
using Resource.UIModels;

namespace Resource.MapperProfiles;

public class CardProfile : Profile {
    public CardProfile()
    {
        CreateMap<Models.Card, CardUI>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id)
            )
            .ForMember(
                dest => dest.Question,
                opt => opt.MapFrom(src => src.Question)
            )
            .ForMember(
                dest => dest.Answer,
                opt => opt.MapFrom(src => src.Answer)
            );
    }
}