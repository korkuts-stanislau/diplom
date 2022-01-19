using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Username { get; set; }
        public Guid AccountId { get; set; }
        public byte[] Icon { get; set; }
        public byte[] Photo { get; set; }
    }
}
