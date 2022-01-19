using System.ComponentModel.DataAnnotations;

namespace MagikAPI.Models.Authentication
{
    public class SignIn
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
