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

        public int FieldId { get; set; }

        public int ProjectTypeId { get; set;}

        public int? OriginalProjectId { get; set;}

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(2048)]
        public string Description { get; set; } = null!;


        public Field? Field { get; set; }

        public ProjectType? ProjectType { get; set; }

        public Project? OriginalProject { get; set; }
        
        public IEnumerable<Stage>? Stages { get; set; }
    }
}
