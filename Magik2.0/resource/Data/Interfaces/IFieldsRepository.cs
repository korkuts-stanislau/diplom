using Resource.Models;

namespace Resource.Data.Interfaces {
    public interface IFieldsRepository {
        public Task<IEnumerable<Field>> GetAsync(string accountId);
        public Task<Field?> FirstOrDefaultAsync(int fieldId);
        public Task CreateAsync(Field field);
        public Task UpdateAsync(Field field);
        public Task DeleteAsync(Field field);
    }
}