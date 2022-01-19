using MagikAPI.Data;
using MagikAPI.Models;
using MagikAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MagikAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectAreasController : ControllerBase
    {
        private readonly MagikContext _context;
        private readonly AccessCheckerService accessCheckerService;

        public ProjectAreasController(MagikContext context, AccessCheckerService accessCheckerService)
        {
            _context = context;
            this.accessCheckerService = accessCheckerService;
        }

        // GET: api/ProjectAreas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectArea>>> GetProjectAreas()
        {
            var currentUserId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

            return await _context.ProjectAreas
                .Where(a => a.AccountId == currentUserId)
                .ToListAsync();
        }

        // GET: api/ProjectAreas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectArea>> GetProjectArea(int id)
        {
            if (!await accessCheckerService.IsUserProjectArea(_context, User, id))
            {
                return BadRequest();
            }

            var projectArea = await _context.ProjectAreas.FindAsync(id);

            if (projectArea == null)
            {
                return NotFound();
            }

            return projectArea;
        }

        // PUT: api/ProjectAreas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectArea(int id, ProjectArea projectArea)
        {
            if (!await accessCheckerService.IsUserProjectArea(_context, User, id))
            {
                return BadRequest();
            }

            if (id != projectArea.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectArea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectAreaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProjectAreas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProjectArea>> PostProjectArea(ProjectArea projectArea)
        {
            var currentUserId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            projectArea.AccountId = currentUserId;

            _context.ProjectAreas.Add(projectArea);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectArea", new { id = projectArea.Id }, projectArea);
        }

        // DELETE: api/ProjectAreas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectArea>> DeleteProjectArea(int id)
        {
            if (!await accessCheckerService.IsUserProjectArea(_context, User, id))
            {
                return BadRequest();
            }

            var projectArea = await _context.ProjectAreas.FindAsync(id);
            if (projectArea == null)
            {
                return NotFound();
            }

            _context.ProjectAreas.Remove(projectArea);
            await _context.SaveChangesAsync();

            return projectArea;
        }

        private bool ProjectAreaExists(int id)
        {
            return _context.ProjectAreas.Any(e => e.Id == id);
        }
    }
}
