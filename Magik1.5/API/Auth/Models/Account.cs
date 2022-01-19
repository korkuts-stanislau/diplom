using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20, MinimumLength = 20)]
        public string PasswordHash { get; set; }

        public Role[] Roles { get; set; }
    }

    public enum Role
    {
        User,
        Admin
    }
}
