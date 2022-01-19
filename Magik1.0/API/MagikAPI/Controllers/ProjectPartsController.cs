using MagikAPI.Data;
using MagikAPI.Models;
using MagikAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagikAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectPartsController : ControllerBase
    {
        private readonly MagikContext _context;
        private readonly AccessCheckerService accessChecker;

        public ProjectPartsController(MagikContext context, AccessCheckerService accessChecker)
        {
            _context = context;
            this.accessChecker = accessChecker;
        }

        // GET: api/ProjectParts
        [HttpGet("forProject/{projectId}")]
        public async Task<ActionResult<IEnumerable<ProjectPart>>> GetProjectPartsForProject(int projectId)
        {
            if (!await accessChecker.IsUserProject(_context, User, projectId))
            {
                return BadRequest();
            }

            return await _context.ProjectParts.Where(p => p.ProjectId == projectId).ToListAsync();
        }

        // GET: api/ProjectParts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectPart>> GetProjectPart(int id)
        {
            var projectPart = await _context.ProjectParts.FindAsync(id);

            if (!await accessChecker.IsUserProject(_context, User, projectPart.ProjectId))
            {
                return BadRequest();
            }

            if (projectPart == null)
            {
                return NotFound();
            }

            return projectPart;
        }

        // PUT: api/ProjectParts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectPart(int id, ProjectPart projectPart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await accessChecker.IsUserProject(_context, User, projectPart.ProjectId))
            {
                return BadRequest();
            }

            if (id != projectPart.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectPart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectPartExists(id))
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

        // POST: api/ProjectParts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProjectPart>> PostProjectPart(ProjectPart projectPart)
        {
            if (!await accessChecker.IsUserProject(_context, User, projectPart.ProjectId))
            {
                return BadRequest();
            }

            projectPart.CreationDate = DateTime.Now;
            projectPart.DeadLine = DateTime.Now.AddDays(7);
            projectPart.Progress = 0;

            _context.ProjectParts.Add(projectPart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectPart", new { id = projectPart.Id }, projectPart);
        }

        // DELETE: api/ProjectParts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectPart>> DeleteProjectPart(int id)
        {
            var projectPart = await _context.ProjectParts.FindAsync(id);
            if (projectPart == null)
            {
                return NotFound();
            }


            if (!await accessChecker.IsUserProject(_context, User, projectPart.ProjectId))
            {
                return BadRequest();
            }

            _context.ProjectParts.Remove(projectPart);
            await _context.SaveChangesAsync();

            return projectPart;
        }

        private bool ProjectPartExists(int id)
        {
            return _context.ProjectParts.Any(e => e.Id == id);
        }
    }
}
