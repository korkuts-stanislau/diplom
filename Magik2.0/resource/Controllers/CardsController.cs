using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resource.Services;
using Resource.UIModels;

namespace Resource.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CardsController : ControllerBase {
    private readonly CardsService cardsService;

    public CardsController(CardsService cardsService)
    {
        this.cardsService = cardsService;
    }

    [HttpGet("{projectId}")]
    [Route("")]
    public async Task<IActionResult> Get(int projectId) {
        var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        try {
            return Ok(await cardsService.GetCardsAsync(accountId, projectId));
        }
        catch(ApplicationException exc) {
            return BadRequest(exc.Message);
        }
    }

    [HttpPost("{projectId}")]
    [Route("")]
    public async Task<IActionResult> Create(int projectId, [FromBody]CardUI card) {
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await cardsService.CreateCardAsync(accountId, projectId, card);
                return Ok(card);
            }
            catch(ApplicationException exc) {
                return BadRequest(exc.Message);
            }
        }
        else {
            return BadRequest();
        }
    }

    [HttpPut("{cardId}")]
    [Route("")]
    public async Task<IActionResult> Update(int cardId, [FromBody]CardUI card) {
        if(cardId != card.Id) return BadRequest();
        if(ModelState.IsValid) {
            var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try {
                await cardsService.UpdateCardAsync(accountId, card);
                return Ok(card);
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
                await cardsService.DeleteCardAsync(accountId, id);
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