using Resource.Models;

namespace Resource.Data.Interfaces {
    public interface ICardsRepository {
        public Task<IEnumerable<Card>> GetAsync(int projectId);
        public Task<Card?> FirstOrDefaultAsync(int cardId);
        public Task CreateAsync(Card card);
        public Task UpdateAsync(Card card);
        public Task DeleteAsync(Card card);
    }
}