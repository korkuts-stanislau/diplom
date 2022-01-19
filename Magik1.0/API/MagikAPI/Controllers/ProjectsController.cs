using MagikAPI.Data;
using MagikAPI.Models;
using MagikAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagikAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly MagikContext _context;
        private readonly AccessCheckerService accessChecker;

        public ProjectsController(MagikContext context, AccessCheckerService accessChecker)
        {
            _context = context;
            this.accessChecker = accessChecker;
        }

        // GET: api/Projects/ForArea/5
        [HttpGet("forArea/{areaId}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsForArea(int areaId)
        {
            if (await accessChecker.IsUserProjectArea(_context, User, areaId))
            {
                return await _context.Projects
                    .Where(p => p.ProjectAreaId == areaId)
                    .ToListAsync();
            }
            else
            {
                return BadRequest();
            }
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            if (!await accessChecker.IsUserProject(_context, User, id))
            {
                return BadRequest();
            }

            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // GET: api/Projects/5
        [HttpGet("progress/{id}")]
        public async Task<ActionResult<int>> GetProjectProgress(int id)
        {
            if (!await accessChecker.IsUserProject(_context, User, id))
            {
                return BadRequest();
            }

            var projectParts = await _context.ProjectParts.Where(p => p.ProjectId == id).Select(p => p.Progress).ToListAsync();

            if (projectParts == null)
            {
                return NotFound();
            }

            return projectParts.Sum() / projectParts.Count;
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (!await accessChecker.IsUserProject(_context, User, id))
            {
                return BadRequest();
            }

            if (id != project.Id)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // POST: api/Projects
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            if (!await accessChecker.IsUserProjectArea(_context, User, project.ProjectAreaId))
            {
                return BadRequest();
            }

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> DeleteProject(int id)
        {
            if (!await accessChecker.IsUserProject(_context, User, id))
            {
                return BadRequest();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return project;
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
