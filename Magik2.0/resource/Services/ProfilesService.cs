using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Resource.Data;
using Resource.Data.Interfaces;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class ProfilesService
{
    private readonly IUnitOfWork uof;
    private readonly PictureConverter converter;
    private readonly IMapper mapper;

    public ProfilesService(IUnitOfWork uof, PictureConverter converter, IMapper mapper)
    {
        this.uof = uof;
        this.converter = converter;
        this.mapper = mapper;
    }

    public async Task<ProfileUI> CreateProfileAsync(string accountId, string email)
    {
        var profile = await uof.Profiles.FirstOrDefaultAsync(accountId);
        if (profile != null) throw new ApplicationException("У этого пользователя уже есть профиль");

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
        if(profile == null) throw new ApplicationException("У этого пользователя нет профиля");

        profile.Username = editedProfile.Username;
        profile.Description = editedProfile.Description;
        if(!string.IsNullOrEmpty(editedProfile.Picture)) {
            profile.Picture = converter.RestrictImage(Convert.FromBase64String(editedProfile.Picture));
            profile.Icon = converter.CreateIconFromImage(profile.Picture);
        }

        await uof.Profiles.UpdateAsync(profile);
    }

    public async Task<IEnumerable<ProfileUI>> GetAcceptedContactProfilesAsync(string accountId) {
        return mapper.Map<IEnumerable<ProfileUI>>(await uof.Profiles.GetAcceptedContactProfilesAsync(accountId));
    }

    public async Task<IEnumerable<ProfileUI>> GetRequestedContactProfilesAsync(string accountId) {
        return mapper.Map<IEnumerable<ProfileUI>>(await uof.Profiles.GetRequestedContactProfilesAsync(accountId));
    }

    public async Task<IEnumerable<ProfileUI>> SearchProfilesByNameAsync(string accountId, string name) {
        return mapper.Map<IEnumerable<ProfileUI>>(await uof.Profiles.SearchProfilesAsync(accountId, IProfilesRepository.SearchFilter.Name, name));
    }

    public async Task<IEnumerable<ProfileUI>> SearchProfilesByDescriptionAsync(string accountId, string description) {
        return mapper.Map<IEnumerable<ProfileUI>>(await uof.Profiles.SearchProfilesAsync(accountId, IProfilesRepository.SearchFilter.Description, description));
    }

    public async Task<ProfileUI?> GetOtherProfileAsync(int profileId) {
        var profile = await uof.Profiles.FirstOrDefaultAsync(profileId);
        if(profile == null) return null;
        return mapper.Map<ProfileUI>(profile);
    }

    public async Task DeleteContactAsync(string accountId, int otherProfileId) {
        await uof.Profiles.DeleteContactAsync(accountId, otherProfileId);
    }

    public async Task DeclineRequestAsync(string accountId, int otherProfileId) {
        await uof.Profiles.DeclineRequestAsync(accountId, otherProfileId);
    }

    public async Task AcceptRequestAsync(string accountId, int otherProfileId) {
        await uof.Profiles.AcceptRequestAsync(accountId, otherProfileId);
    }

    public async Task SendRequestAsync(string accountId, int otherProfileId) {
        await uof.Profiles.SendRequestAsync(accountId, otherProfileId);
    }
}