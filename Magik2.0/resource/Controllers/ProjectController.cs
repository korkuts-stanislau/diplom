using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Resource.Services;
using Resource.UIModels;

namespace Resource.Controllers;

public class ProjectController : ControllerBase {
    private readonly ProjectService projectService;

    public ProjectController(ProjectService projectService)
    {
        this.projectService = projectService;
    }

    [HttpGet("{areaId}")]
    [Route("")]
    public async Task<IActionResult> Get(int areaId) {
        var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        try {
            return Ok(await projectService.GetProjectsAsync(accountId, areaId));
        }
        catch(Exception exc) {
            return BadRequest(exc.Message);
        }
    }

    [HttpPost("{areaId}")]
    [Route("")]
    public async Task<IActionResult> Create(int areaId, [FromBody]ProjectUI project) {
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await projectService.CreateProjectAsync(accountId, areaId, project);
                return Ok(project.Id);
            }
            catch(Exception exc) {
                return BadRequest(exc.Message);
            }
        }
        else {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("")]
    public async Task<IActionResult> Update([FromBody]ProjectUI project) {
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await projectService.UpdateProjectAsync(accountId, project);
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

    [HttpDelete("{id}")]
    [Route("")]
    public async Task<IActionResult> Delete(int id) {
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await projectService.DeleteProjectAsync(accountId, id);
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