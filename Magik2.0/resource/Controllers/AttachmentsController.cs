using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resource.Services;
using Resource.UIModels;

namespace Resource.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AttachmentsController : ControllerBase {
    private readonly AttachmentsService attachmentsService;

    public AttachmentsController(AttachmentsService attachmentsService)
    {
        this.attachmentsService = attachmentsService;
    }

    [HttpGet("stage/{stageId}")]
    public async Task<IActionResult> GetForStage(int stageId)
    {
        var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        try {
            return Ok(await attachmentsService.GetStageAttachments(accountId, stageId));
        }
        catch(ApplicationException exc) {
            return BadRequest(exc.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        try {
            return Ok(await attachmentsService.GetAccountAttachments(accountId));
        }
        catch(ApplicationException exc) {
            return BadRequest(exc.Message);
        }
    }

    [HttpPost("stage/{stageId}")]
    public async Task<IActionResult> AddToStage(int stageId, [FromBody]int attachId) {
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await attachmentsService.AddAttachmentToStageAsync(accountId, stageId, attachId);
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]AttachmentUI attach) {
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await attachmentsService.CreateNewAttachmentAsync(accountId, attach);
                return Ok(attach);
            }
            catch(ApplicationException exc) {
                return BadRequest(exc.Message);
            }
        }
        else {
            return BadRequest();
        }
    }

    [HttpPut("{attachId}")]
    public async Task<IActionResult> Update(int attachId, [FromBody]AttachmentUI attach) {
        if(attachId != attach.Id) return BadRequest();
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await attachmentsService.UpdateAttachmentAsync(accountId, attach);
                return Ok(attach);
            }
            catch(ApplicationException exc) {
                return BadRequest(exc.Message);
            }
        }
        else {
            return BadRequest();
        }
    }

    [HttpDelete("stage/{stageId}")]
    public async Task<IActionResult> DeleteFromStage(int stageId, [FromQuery]int attachmentId) {
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await attachmentsService.DeleteAttachmentFromStageAsync(accountId, stageId, attachmentId);
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
    public async Task<IActionResult> Delete(int id) {
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await attachmentsService.DeleteAttachmentAsync(accountId, id);
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