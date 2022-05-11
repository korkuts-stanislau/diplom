using Resource.Models;

namespace Resource.Data.Interfaces
{
    public interface IProfilesRepository {
        public Task<Profile?> FirstOrDefaultAsync(string accountId);
        public Task<Profile?> FirstOrDefaultAsync(int profileId);
        public Task CreateAsync(Profile profile);
        public Task UpdateAsync(Profile profile);
        public Task<IEnumerable<Profile>> GetAcceptedContactProfilesAsync(string accountId);
        public Task<IEnumerable<Profile>> GetRequestedContactProfilesAsync(string accountId);
        public Task<IEnumerable<Profile>> SearchProfilesAsync(string accountId, SearchFilter filter, string query);
        public Task DeleteContactAsync(string accountId, int otherProfileId);
        public Task DeclineRequestAsync(string accountId, int otherProfileId);
        public Task AcceptRequestAsync(string accountId, int otherProfileId);
        public Task SendRequestAsync(string accountId, int otherProfileId);

        public enum SearchFilter {
            Name,
            Description
        }
    }
}