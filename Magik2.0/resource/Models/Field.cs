using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Models
{
    public class Field
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(24)]
        public string AccountId { get; set; } = null!;

        public byte[]? Icon { get; set; }
        

        public IEnumerable<Project>? Projects { get; set; }
    }
}
