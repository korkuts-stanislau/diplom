using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Resource.Data;
using Resource.Data.Interfaces;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class ProfileService
{
    private readonly IUnitOfWork uof;
    private readonly PictureConverter converter;
    private readonly IMapper mapper;

    public ProfileService(IUnitOfWork uof, PictureConverter converter, IMapper mapper)
    {
        this.uof = uof;
        this.converter = converter;
        this.mapper = mapper;
    }

    public async Task<ProfileUI> CreateProfileAsync(string accountId, string email)
    {
        var profile = await uof.Profiles.FirstOrDefaultAsync(accountId);
        if (profile != null) throw new Exception("У этого пользователя уже есть профиль");

        profile = new Models.Profile
        {
            AccountId = accountId,
            Username = email.Split("@")[0],
            Description = "😊",
            Icon = null,
            Picture = null
        };

        await uof.Profiles.CreateAsync(profile);

        return mapper.Map<ProfileUI>(profile);
    }

    public async Task<ProfileUI?> GetProfileOrDefaultAsync(string accountId)
    {
        var profile = await uof.Profiles.FirstOrDefaultAsync(accountId);
        if (profile == null) return null;
        
        return mapper.Map<ProfileUI>(profile);
    }

    public async Task UpdateProfileAsync(string accountId, UIModels.ProfileUI editedProfile) {
        var profile = await uof.Profiles.FirstOrDefaultAsync(accountId);
        if(profile == null) throw new Exception("У этого пользователя нет профиля");

        profile.Username = editedProfile.Username;
        profile.Description = editedProfile.Description;
        if(!string.IsNullOrEmpty(editedProfile.Picture)) {
            profile.Picture = converter.RestrictImage(Convert.FromBase64String(editedProfile.Picture));
            profile.Icon = converter.CreateIconFromImage(profile.Picture);
        }

        await uof.Profiles.UpdateAsync(profile);
    }
}