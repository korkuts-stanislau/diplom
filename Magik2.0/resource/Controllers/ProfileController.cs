using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resource.Data;
using Resource.Services;

namespace Resource.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly ProfileService _profileService;

    public ProfileController(ProfileService profileService)
    {
        _profileService = profileService;
    }
    
    [Route("")]
    [HttpGet]
    public async Task<IActionResult> GetCurrentAccountProfile()
    {
        var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var profile = await _profileService.GetAccountProfile(accountId);
        
        if (profile == null)
        {
            var email = User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
            profile = await _profileService.CreateNewAccountProfile(accountId, email);
        }

        return Ok(profile);
    }
}