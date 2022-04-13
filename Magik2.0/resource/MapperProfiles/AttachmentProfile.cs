using AutoMapper;
using Resource.UIModels;

namespace Resource.MapperProfiles;

public class AttachmentMapperProfile : Profile {
    public AttachmentMapperProfile()
    {
        CreateMap<Models.AccountAttachment, AttachmentUI>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id)
            )
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name)
            )
            .ForMember(
                dest => dest.AttachmentTypeId,
                opt => opt.MapFrom(src => src.AttachmentTypeId)
            )
            .ForMember(
                dest => dest.Data,
                opt => opt.MapFrom(src => src.Data)
            );
    }
}