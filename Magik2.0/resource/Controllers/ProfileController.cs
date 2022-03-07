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
    private readonly ProfileService profileService;

    public ProfileController(ProfileService profileService)
    {
        this.profileService = profileService;
    }
    
    [Route("")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var profile = await profileService.GetProfileOrDefaultAsync(accountId);
        
        if (profile == null)
        {
            try {
                var email = User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
                profile = await profileService.CreateProfileAsync(accountId, email);
            }
            catch(Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        return Ok(profile);
    }

    [Route("")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody]UIModels.ProfileUI profile)
    {
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await profileService.UpdateProfileAsync(accountId, profile);
                return Ok();
            }
            catch(Exception exc) {
                return BadRequest(exc.Message);
            }
        }
        else {
            return BadRequest();
        }
    }
}