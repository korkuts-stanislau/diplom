using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Models
{
    public class ProjectPart
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime Deadline { get; set; }

        [Range(0, 100)]
        public int Progress { get; set; }
        

        public Project Project { get; set; }
    }
}
