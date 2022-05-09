using Resource.Models;

namespace Resource.Data.Interfaces
{
    public interface IProfilesRepository {
        public Task<Profile?> FirstOrDefaultAsync(string accountId);
        public Task CreateAsync(Profile profile);
        public Task UpdateAsync(Profile profile);
        public Task<IEnumerable<Profile>> GetAcceptedContactProfilesAsync(string accountId);
        public Task<IEnumerable<Profile>> GetRequestedContactProfilesAsync(string accountId);
        public Task<IEnumerable<Profile>> SearchProfiles(string accountId, SearchFilter filter, string query);

        public enum SearchFilter {
            Name,
            Description
        }
    }
}