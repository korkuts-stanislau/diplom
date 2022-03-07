using Microsoft.EntityFrameworkCore;
using Resource.Data.Interfaces;
using Resource.Models;

namespace Resource.Data.MSImplementations {
    public class MSProjectAreaRepository : IProjectAreaRepository
    {
        private readonly AppDbContext context;

        public MSProjectAreaRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task CreateAsync(ProjectArea area)
        {
            await context.ProjectAreas.AddAsync(area);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProjectArea area)
        {
            context.ProjectAreas.Remove(area);
            await context.SaveChangesAsync();
        }

        public async Task<ProjectArea?> FirstOrDefaultAsync(int areaId)
        {
            return await context.ProjectAreas.FirstOrDefaultAsync(a => a.Id == areaId);
        }

        public async Task<IEnumerable<ProjectArea>> GetAsync(string accountId)
        {
            return await context.ProjectAreas
                .Where(p => p.AccountId == accountId)
                .ToListAsync();
        }

        public async Task UpdateAsync(ProjectArea area)
        {
            context.ProjectAreas.Update(area);
            await context.SaveChangesAsync();
        }
    }
}