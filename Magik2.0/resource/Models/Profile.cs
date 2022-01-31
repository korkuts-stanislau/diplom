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
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(24)]
        public string AccountId { get; set; }
        public byte[] Icon { get; set; }
        public byte[] Photo { get; set; }
    }
}
