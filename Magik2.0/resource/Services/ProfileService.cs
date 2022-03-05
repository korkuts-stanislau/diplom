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

    /// <summary>
    /// Service for accounts' profiles management
    /// </summary>
    /// <param name="rep">Profile repository</param>
    /// <param name="converter">Pictures converter</param>
    public ProfileService(IProfileRepository rep, PictureConverter converter)
    {
        this.rep = rep;
        this.converter = converter;
    }

    /// <summary>
    /// Get account profile
    /// </summary>
    /// <param name="accountId">Account ID</param>
    /// <returns>Account profile</returns>
    public async Task<ProfileUI?> GetProfileOrDefaultAsync(string accountId)
    {
        var profile = await rep.FirstOrDefaultAsync(accountId);
        if (profile == null) return null;
        
        return new ProfileUI
        {
            Username = profile.Username,
            Description = profile.Description,
            Picture = profile.Picture != null ? Convert.ToBase64String(profile.Picture) : ""
        };
    }

    /// <summary>
    /// Create profile for account
    /// </summary>
    /// <param name="accountId">Account ID</param>
    /// <param name="email">User email</param>
    /// <returns>Created profile</returns>
    public async Task<ProfileUI> CreateProfileAsync(string accountId, string email)
    {
        var profile = await rep.FirstOrDefaultAsync(accountId);
        if (profile != null) throw new Exception("У этого пользователя уже есть профиль");

        profile = new Models.Profile
        {
            AccountId = accountId,
            Username = email,
            Description = "Мой профиль",
            Icon = null,
            Picture = null
        };

        await rep.CreateAsync(profile);

        return new ProfileUI
        {
            Username = profile.Username,
            Description = profile.Description,
            Picture = profile.Picture != null ? Convert.ToBase64String(profile.Picture) : ""
        };
    }

    /// <summary>
    /// Update account profile
    /// </summary>
    /// <param name="accountId">Account ID</param>
    /// <param name="editedProfile">Profile update data</param>
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