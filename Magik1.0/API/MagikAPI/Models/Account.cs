using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MagikAPI.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(64)]
        public string Email { get; set; }
        [Required]
        [MaxLength(32)]
        public string PasswordHash { get; set; }
        public Profile Profile { get; set; }
        public IEnumerable<ProjectArea> ProjectAreas { get; set; }
    }
}
