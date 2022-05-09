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

    [Route("contacts/accepted")]
    [HttpGet]
    public async Task<IActionResult> GetAcceptedContacts()
    {
        var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        
        try {
            return Ok(await profilesService.GetAcceptedContactProfilesAsync(accountId));
        }
        catch(ApplicationException exc) {
            return BadRequest(exc.Message);
        }
    }

    [Route("contacts/requested")]
    [HttpGet]
    public async Task<IActionResult> GetRequestedContacts()
    {
        var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        
        try {
            return Ok(await profilesService.GetRequestedContactProfilesAsync(accountId));
        }
        catch(ApplicationException exc) {
            return BadRequest(exc.Message);
        }
    }

    [Route("contacts/search")]
    [HttpGet]
    public async Task<IActionResult> SearchContacts([FromQuery]string? name, [FromQuery]string? description)
    {
        var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        
        try {
            if(!string.IsNullOrEmpty(name)) {
                return Ok(await profilesService.SearchProfilesByName(accountId, name));
            }
            if(!string.IsNullOrEmpty(description)) {
                return Ok(await profilesService.SearchProfilesByDescription(accountId, description));
            }
            return BadRequest("Передайте параметр поиска");
        }
        catch(ApplicationException exc) {
            return BadRequest(exc.Message);
        }
    }
}