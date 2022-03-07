using Microsoft.EntityFrameworkCore;
using Resource.Data.Interfaces;
using Resource.Models;

namespace Resource.Data.MSImplementations {
    public class MSProfileRepository : IProfileRepository
    {
        private readonly AppDbContext context;

        public MSProfileRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task CreateAsync(Profile profile)
        {
            await context.Profiles.AddAsync(profile);
            await context.SaveChangesAsync();
        }

        public async Task<Profile?> FirstOrDefaultAsync(string accountId)
        {
            return await context.Profiles.FirstOrDefaultAsync(p => p.AccountId == accountId);
        }

        public async Task UpdateAsync(Profile profile)
        {
            context.Profiles.Update(profile);
            await context.SaveChangesAsync();
        }
    }
}