using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Resource.Data.Interfaces;
using Resource.Models;

namespace Resource.Data.MSImplementations;

public class MSStagesAttachmentsRepository : IStagesAttachmentsRepository
{
    private readonly AppDbContext context;

    public MSStagesAttachmentsRepository(AppDbContext context)
    {
        this.context = context;

    }

    public async Task CreateAsync(StageAttachment attachment)
    {
        await context.StagesAttachments.AddAsync(attachment);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(StageAttachment attachment)
    {
        context.StagesAttachments.Remove(attachment);
        await context.SaveChangesAsync();
    }

    public async Task<StageAttachment?> FirstOrDefaultAsync(int attachmentId)
    {
        return await context.StagesAttachments.FirstOrDefaultAsync(s => s.Id == attachmentId);
    }

    public async Task<IEnumerable<StageAttachment>> WhereAsync(Expression<Func<StageAttachment, bool>> predicate)
    {
        return await context.StagesAttachments
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<IEnumerable<StageAttachment>> GetAsync(int stageId)
    {
        return await context.StagesAttachments
            .Where(s => s.StageId == stageId)
            .Include(s => s.AccountAttachment)
            .ToListAsync();
    }

    public async Task UpdateAsync(StageAttachment attachment)
    {
        context.Update(attachment);
        await context.SaveChangesAsync();
    }
}