﻿using Microsoft.EntityFrameworkCore;
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

    public async Task<Profile> GetAccountProfile(string accountId)
    {
        var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.AccountId == accountId);
        if (profile == null) return null;
        return new Profile
        {
            Id = profile.Id,
            Username = profile.Username,
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
            Icon = new byte[0],
            Picture = new byte[0]
        };
        await _context.Profiles.AddAsync(profile);
        await _context.SaveChangesAsync();
        return new Profile
        {
            Id = profile.Id,
            Username = profile.Username,
            Picture = profile.Picture.Length != 0 ? Convert.ToBase64String(profile.Picture) : ""
        };
    }
}