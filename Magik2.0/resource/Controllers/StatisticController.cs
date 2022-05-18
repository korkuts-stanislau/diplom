using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resource.Services;

namespace Resource.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StatisticController : ControllerBase {
    private readonly StatisticService statisticService;

    public StatisticController(StatisticService statisticService)
    {
        this.statisticService = statisticService;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Get() {
        var accountId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        try {
            return Ok(await statisticService.GenerateStatisticForUserAsync(accountId));
        }
        catch(ApplicationException exc) {
            return BadRequest(exc.Message);
        }
    }
}