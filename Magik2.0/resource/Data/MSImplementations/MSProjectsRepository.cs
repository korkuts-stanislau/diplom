using Microsoft.EntityFrameworkCore;
using Resource.Data.Interfaces;
using Resource.Models;

namespace Resource.Data.MSImplementations;

public class MSProjectsRepository : IProjectsRepository
{
    private readonly AppDbContext context;

    public MSProjectsRepository(AppDbContext context)
    {
        this.context = context;
    }
    public async Task CreateAsync(Project project)
    {
        await context.Projects.AddAsync(project);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Project project)
    {
        context.Projects.Remove(project);
        await context.SaveChangesAsync();
    }

    public async Task<Project?> FirstOrDefaultAsync(int projectId)
    {
        return await context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
    }

    public async Task<IEnumerable<Project>> GetAsync(int fieldId)
    {
        return await context.Projects.Where(p => p.FieldId == fieldId)
            .ToListAsync();
    }

    public async Task UpdateAsync(Project project)
    {
        context.Projects.Update(project);
        await context.SaveChangesAsync();
    }
}