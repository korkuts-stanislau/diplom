using Resource.Models;

namespace Resource.Data.Interfaces
{
    public interface IProfilesRepository {
        public Task<Profile?> FirstOrDefaultAsync(string accountId);
        public Task CreateAsync(Profile profile);
        public Task UpdateAsync(Profile profile);
    }
}