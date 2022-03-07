using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Resource.Data;
using Resource.Data.Interfaces;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class ProfileService
{
    private readonly IProfileRepository rep;
    private readonly PictureConverter converter;
    private readonly IMapper mapper;

    public ProfileService(IProfileRepository rep, PictureConverter converter, IMapper mapper)
    {
        this.rep = rep;
        this.converter = converter;
        this.mapper = mapper;
    }

    public async Task<ProfileUI?> GetProfileOrDefaultAsync(string accountId)
    {
        var profile = await rep.FirstOrDefaultAsync(accountId);
        if (profile == null) return null;
        
        return mapper.Map<ProfileUI>(profile);
    }

    public async Task<ProfileUI> CreateProfileAsync(string accountId, string email)
    {
        var profile = await rep.FirstOrDefaultAsync(accountId);
        if (profile != null) throw new Exception("У этого пользователя уже есть профиль");

        profile = new Models.Profile
        {
            AccountId = accountId,
            Username = email.Split("@")[0],
            Description = "😊",
            Icon = null,
            Picture = null
        };

        await rep.CreateAsync(profile);

        return mapper.Map<ProfileUI>(profile);
    }

    public async Task UpdateProfileAsync(string accountId, UIModels.ProfileUI editedProfile) {
        var profile = await rep.FirstOrDefaultAsync(accountId);
        if(profile == null) throw new Exception("У этого пользователя нет профиля");

        profile.Username = editedProfile.Username;
        profile.Description = editedProfile.Description;
        if(!string.IsNullOrEmpty(editedProfile.Picture)) {
            profile.Picture = converter.RestrictImage(Convert.FromBase64String(editedProfile.Picture));
            profile.Icon = converter.CreateIconFromImage(profile.Picture);
        }

        await rep.UpdateAsync(profile);
    }
}