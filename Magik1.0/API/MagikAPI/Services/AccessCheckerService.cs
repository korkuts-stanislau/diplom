using MagikAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MagikAPI.Services
{
    public class AccessCheckerService
    {
        public async Task<bool> IsUserProject(MagikContext context, ClaimsPrincipal user, int projectId)
        {
            var currentUserId = int.Parse(user.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var allUserProjectIds = context.ProjectAreas
                .Where(area => area.AccountId == currentUserId)
                .Join(context.Projects, a => a.Id, p => p.ProjectAreaId, (a, p) => p.Id);
            return (await allUserProjectIds.FirstOrDefaultAsync(id => id == projectId)) != default;
        }

        public async Task<bool> IsUserProjectArea(MagikContext context, ClaimsPrincipal user, int projectAreaId)
        {
            var currentUserId = int.Parse(user.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var allUserProjectAreasIds = context.ProjectAreas
                .Where(area => area.AccountId == currentUserId)
                .Select(area => area.Id);
            return (await allUserProjectAreasIds.FirstOrDefaultAsync(id => id == projectAreaId)) != default;
        }
    }
}
