using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resource.Services;
using Resource.UIModels;

namespace Resource.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProjectsController : ControllerBase {
    private readonly ProjectsService projectsService;

    public ProjectsController(ProjectsService projectsService)
    {
        this.projectsService = projectsService;
    }

    [HttpGet("{fieldId}")]
    [Route("")]
    public async Task<IActionResult> Get(int fieldId) {
        var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        try {
            return Ok(await projectsService.GetProjectsAsync(accountId, fieldId));
        }
        catch(ApplicationException exc) {
            return BadRequest(exc.Message);
        }
    }

    [HttpPost("{fieldId}")]
    [Route("")]
    public async Task<IActionResult> Create(int fieldId, [FromBody]ProjectUI project) {
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await projectsService.CreateProjectAsync(accountId, fieldId, project);
                return Ok(project);
            }
            catch(ApplicationException exc) {
                return BadRequest(exc.Message);
            }
        }
        else {
            return BadRequest();
        }
    }

    [HttpPut("{projectId}")]
    [Route("")]
    public async Task<IActionResult> Update(int projectId, [FromBody]ProjectUI project) {
        if(projectId != project.Id) return BadRequest();
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await projectsService.UpdateProjectAsync(accountId, project);
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

    [HttpDelete("{id}")]
    [Route("")]
    public async Task<IActionResult> Delete(int id) {
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await projectsService.DeleteProjectAsync(accountId, id);
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