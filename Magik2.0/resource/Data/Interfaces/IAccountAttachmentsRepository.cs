using Resource.Models;

namespace Resource.Data.Interfaces
{
    public interface IAccountAttachmentsRepository {
        public Task<IEnumerable<AccountAttachment>> GetAsync(string accountId);
        public Task<AccountAttachment?> FirstOrDefaultAsync(int attachmentId);
        public Task CreateAsync(AccountAttachment attachment);
        public Task UpdateAsync(AccountAttachment attachment);
        public Task DeleteAsync(AccountAttachment attachment);
    }
}