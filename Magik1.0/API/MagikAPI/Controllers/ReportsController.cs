using MagikAPI.Data;
using MagikAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MagikAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly MagikContext context;
        private readonly ReportsService reportsService;

        public ReportsController(MagikContext context, ReportsService reportsService)
        {
            this.context = context;
            this.reportsService = reportsService;
        }

        [HttpGet("projects")]
        public async Task<IActionResult> GetProjectsReport()
        {
            var currentUserId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

            return Ok(await reportsService.GetFullProjectsReport(context, currentUserId));
        }
    }
}
