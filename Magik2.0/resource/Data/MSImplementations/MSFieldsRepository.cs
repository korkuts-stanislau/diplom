using Microsoft.EntityFrameworkCore;
using Resource.Data.Interfaces;
using Resource.Models;

namespace Resource.Data.MSImplementations {
    public class MSFieldsRepository : IFieldsRepository
    {
        private readonly AppDbContext context;

        public MSFieldsRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task CreateAsync(Field field)
        {
            await context.Fields.AddAsync(field);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Field field)
        {
            context.Fields.Remove(field);
            await context.SaveChangesAsync();
        }

        public async Task<Field?> FirstOrDefaultAsync(int fieldId)
        {
            return await context.Fields.FirstOrDefaultAsync(f => f.Id == fieldId);
        }

        public async Task<IEnumerable<Field>> GetAsync(string accountId)
        {
            return await context.Fields
                .Where(f => f.AccountId == accountId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Field field)
        {
            context.Fields.Update(field);
            await context.SaveChangesAsync();
        }
    }
}