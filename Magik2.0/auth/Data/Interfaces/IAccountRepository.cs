using Auth.Models;

namespace Auth.Data.Interfaces
{
    public interface IAccountRepository
    {
        public Task<Account> GetByIdAsync(string id);

        public Task<Account> GetByEmailAsync(string email);
        
        public Task CreateAsync(Account a);
    }
}