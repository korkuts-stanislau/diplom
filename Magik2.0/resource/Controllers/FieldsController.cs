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
public class FieldsController : ControllerBase {
    private readonly FieldsService fieldsService;

    public FieldsController(FieldsService fieldsService)
    {
        this.fieldsService = fieldsService;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Get() {
        var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        return Ok(await fieldsService.GetFieldsAsync(accountId));
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Create([FromBody]FieldUI field) {
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await fieldsService.CreateFieldAsync(accountId, field);
                return Ok(field.Id);
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
    public async Task<IActionResult> Update([FromBody]FieldUI field) {
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await fieldsService.UpdateFieldAsync(accountId, field);
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
        var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        try {
            await fieldsService.DeleteFieldAsync(accountId, id);
            return Ok();
        }
        catch(Exception exc) {
            return BadRequest(exc.Message);
        }
    }
}