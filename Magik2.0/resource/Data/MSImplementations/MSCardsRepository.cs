using Microsoft.EntityFrameworkCore;
using Resource.Data.Interfaces;
using Resource.Models;

namespace Resource.Data.MSImplementations {
    public class MSCardsRepository : ICardsRepository
    {
        private readonly AppDbContext context;

        public MSCardsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Card>> GetAsync(int projectId)
        {
            return await context.Cards
                .Where(c => c.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task CreateAsync(Card card)
        {
            await context.Cards.AddAsync(card);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Card card)
        {
            context.Cards.Remove(card);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Card card)
        {
            context.Cards.Update(card);
            await context.SaveChangesAsync();
        }

        public async Task<Card?> FirstOrDefaultAsync(int cardId)
        {
            return await context.Cards.FirstOrDefaultAsync(c => c.Id == cardId);
        }
    }
}