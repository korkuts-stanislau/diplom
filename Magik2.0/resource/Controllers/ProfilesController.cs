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
public class ProfilesController : ControllerBase
{
    private readonly ProfilesService profilesService;

    public ProfilesController(ProfilesService profilesService)
    {
        this.profilesService = profilesService;
    }
    
    [Route("")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var profile = await profilesService.GetProfileOrDefaultAsync(accountId);
        
        if (profile == null)
        {
            try {
                var email = User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
                profile = await profilesService.CreateProfileAsync(accountId, email);
            }
            catch(ApplicationException exc) {
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
                await profilesService.UpdateProfileAsync(accountId, profile);
                return Ok();
            }
            catch(ApplicationException exc) {
                return BadRequest(exc.Message);
            }
        }
        else {
            return BadRequest();
        }
    }
}