using System.ComponentModel.DataAnnotations;

namespace MagikAPI.Models
{
    public class Profile
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(64)]
        public string UserName { get; set; }
        public byte[] Photo { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
