using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Models
{
    public class Project
    {
        public int Id { get; set; }

        public int ProjectAreaId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }


        public ProjectArea ProjectArea { get; set; }
        
        public IEnumerable<ProjectPart> ProjectParts { get; set; }
    }
}
