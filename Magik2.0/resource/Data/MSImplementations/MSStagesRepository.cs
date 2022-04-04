using Microsoft.EntityFrameworkCore;
using Resource.Data.Interfaces;
using Resource.Models;

namespace Resource.Data.MSImplementations;

public class MSStagesRepository : IStagesRepository
{
    private readonly AppDbContext context;

    public MSStagesRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task CreateAsync(Stage stage)
    {
        await context.Stages.AddAsync(stage);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Stage stage)
    {
        context.Stages.Remove(stage);
        await context.SaveChangesAsync();
    }

    public async Task<Stage?> FirstOrDefaultAsync(int stageId)
    {
        return await context.Stages.FirstOrDefaultAsync(s => s.Id == stageId);
    }

    public async Task<IEnumerable<Stage>> GetAsync(int projectId)
    {
        return await context.Stages
            .Where(s => s.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task UpdateAsync(Stage stage)
    {
        context.Update(stage);
        await context.SaveChangesAsync();
    }
}