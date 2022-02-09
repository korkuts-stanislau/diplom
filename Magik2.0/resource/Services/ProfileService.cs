using Microsoft.EntityFrameworkCore;
using Resource.Data;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class ProfileService
{
    private readonly AppDbContext _context;
    private readonly PictureConverter _converter;

    public ProfileService(AppDbContext context, PictureConverter converter)
    {
        _context = context;
        _converter = converter;
    }

    public async Task<Profile> GetAccountProfileOrDefault(string accountId)
    {
        var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.AccountId == accountId);
        if (profile == null) return default;
        return new Profile
        {
            Username = profile.Username,
            Description = profile.Description,
            Picture = profile.Picture.Length  != 0 ? Convert.ToBase64String(profile.Picture) : ""
        };
    }

    public async Task<Profile> CreateNewAccountProfile(string accountId, string email)
    {
        var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.AccountId == accountId);
        if (profile != null) throw new Exception("У этого пользователя уже есть профиль");
        profile = new Models.Profile
        {
            AccountId = accountId,
            Username = email,
            Description = "Мой профиль",
            Icon = new byte[0],
            Picture = new byte[0]
        };
        await _context.Profiles.AddAsync(profile);
        await _context.SaveChangesAsync();
        return new Profile
        {
            Username = profile.Username,
            Description = profile.Description,
            Picture = profile.Picture.Length != 0 ? Convert.ToBase64String(profile.Picture) : ""
        };
    }

    public async Task EditAccountProfile(string accountId, UIModels.Profile editedProfile) {
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