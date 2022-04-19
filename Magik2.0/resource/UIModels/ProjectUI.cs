using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.UIModels
{
    public class ProjectUI
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(2048)]
        public string Description { get; set; } = null!;

        public string? Color { get; set; } = null!;
    }
}
