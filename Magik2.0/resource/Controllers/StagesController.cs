using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resource.Services;
using Resource.UIModels;

namespace Resource.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StagesController : ControllerBase {
    private readonly StagesService stagesService;

    public StagesController(StagesService stagesService)
    {
        this.stagesService = stagesService;
    }

    [HttpGet("{projectId}")]
    [Route("")]
    public async Task<IActionResult> Get(int projectId) {
        var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        try {
            return Ok(await stagesService.GetStagesAsync(accountId, projectId));
        }
        catch(ApplicationException exc) {
            return BadRequest(exc.Message);
        }
    }

    [HttpPost("{projectId}")]
    [Route("")]
    public async Task<IActionResult> Create(int projectId, [FromBody]StageUI stage) {
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await stagesService.CreateStageAsync(accountId, projectId, stage);
                return Ok(stage);
            }
            catch(ApplicationException exc) {
                return BadRequest(exc.Message);
            }
        }
        else {
            return BadRequest();
        }
    }

    [HttpPut("{stageId}")]
    [Route("")]
    public async Task<IActionResult> Update(int stageId, [FromBody]StageUI stage) {
        if(stageId != stage.Id) return BadRequest();
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await stagesService.UpdateStageAsync(accountId, stage);
                return Ok(stage);
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
                await stagesService.DeleteStageAsync(accountId, id);
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