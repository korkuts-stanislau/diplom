using Microsoft.EntityFrameworkCore;
using Resource.Data;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class ProfileService
{
    private readonly AppDbContext _context;
    private readonly PictureConverter _converter;

    /// <summary>
    /// Service for accounts' profiles management
    /// </summary>
    /// <param name="context">Data context</param>
    /// <param name="converter">Pictures converter</param>
    public ProfileService(AppDbContext context, PictureConverter converter)
    {
        _context = context;
        _converter = converter;
    }

    /// <summary>
    /// Get account profile
    /// </summary>
    /// <param name="accountId">Account ID</param>
    /// <returns>Account profile</returns>
    public async Task<Profile?> GetProfileOrDefault(string accountId)
    {
        var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.AccountId == accountId);
        if (profile == null) return null;
        
        return new Profile
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
    public async Task<Profile> CreateProfile(string accountId, string email)
    {
        var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.AccountId == accountId);
        if (profile != null) throw new Exception("У этого пользователя уже есть профиль");

        profile = new Models.Profile
        {
            AccountId = accountId,
            Username = email,
            Description = "Мой профиль",
            Icon = null,
            Picture = null
        };

        await _context.Profiles.AddAsync(profile);
        await _context.SaveChangesAsync();

        return new Profile
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
    public async Task UpdateProfile(string accountId, UIModels.Profile editedProfile) {
        var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.AccountId == accountId);
        if(profile == null) throw new Exception("У этого пользователя нет профиля");

        profile.Username = editedProfile.Username;
        profile.Description = editedProfile.Description;
        if(!string.IsNullOrEmpty(editedProfile.Picture)) {
            profile.Picture = _converter.RestrictImage(Convert.FromBase64String(editedProfile.Picture));
            profile.Icon = _converter.CreateIconFromImage(profile.Picture);
        }
        _context.Profiles.Update(profile);
        await _context.SaveChangesAsync();
    }
}