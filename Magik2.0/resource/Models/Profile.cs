using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [StringLength(24)]
        public string AccountId { get; set; } = null!;

        [StringLength(50)]
        public string Username { get; set; } = null!;

        [StringLength(250)]
        public string Description { get; set; } = null!;

        public byte[]? Icon { get; set; }
        
        public byte[]? Picture { get; set; }
    }
}
