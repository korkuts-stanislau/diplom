using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Models
{
    public class ProjectArea
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public Guid AccountId { get; set; }
        public byte[] Icon { get; set; }

        public IEnumerable<Project> Projects { get; set; }
    }
}
