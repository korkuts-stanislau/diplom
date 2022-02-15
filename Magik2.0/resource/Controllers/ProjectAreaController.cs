using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resource.Data;
using Resource.Services;
using Resource.UIModels;

namespace Resource.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProjectAreaController : ControllerBase {
    private readonly ProjectAreaService projectAreaService;

    public ProjectAreaController(ProjectAreaService projectAreaService)
    {
        this.projectAreaService = projectAreaService;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetProjectAreas() {
        var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        return Ok(await projectAreaService.GetProjectAreas(accountId));
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddProjectArea([FromBody]ProjectArea area) {
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                int newAreaId = await projectAreaService.CreateNewProjectArea(area, accountId);
                return Ok(newAreaId);
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