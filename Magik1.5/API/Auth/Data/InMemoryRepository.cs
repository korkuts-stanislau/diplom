using Auth.Models;
using Auth.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Data
{
    public class InMemoryRepository
    {
        private readonly PasswordHasherService passwordHasher;

        public InMemoryRepository(PasswordHasherService passwordHasher)
        {
            this.passwordHasher = passwordHasher;
        }
        private List<Account> accounts;
        public List<Account> Accounts => accounts ??= new List<Account>
        {
            new Account()
            {
                Id = Guid.NewGuid(),
                Email = "stas@gmail.com",
                PasswordHash = passwordHasher.Hash("staslol"),
                Roles = new Role[] { Role.Admin }
            }
        };
    }
}