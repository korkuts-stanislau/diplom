using Microsoft.EntityFrameworkCore;
using Resource.Data.Interfaces;
using Resource.Models;

namespace Resource.Data.MSImplementations;

public class MSAccountAttachmentsRepository : IAccountAttachmentsRepository
{
    private readonly AppDbContext context;

    public MSAccountAttachmentsRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task CreateAsync(AccountAttachment attachment)
    {
        await context.AccountAttachments.AddAsync(attachment);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(AccountAttachment attachment)
    {
        context.AccountAttachments.Remove(attachment);
        await context.SaveChangesAsync();
    }

    public async Task<AccountAttachment?> FirstOrDefaultAsync(int attachmentId)
    {
        return await context.AccountAttachments.FirstOrDefaultAsync(a => a.Id == attachmentId);
    }

    public async Task<IEnumerable<AccountAttachment>> GetAsync(string accountId)
    {
        return await context.AccountAttachments
            .Where(a => a.AccountId == accountId)
            .ToListAsync();
    }

    public async Task UpdateAsync(AccountAttachment attachment)
    {
        context.Update(attachment);
        await context.SaveChangesAsync();
    }
}