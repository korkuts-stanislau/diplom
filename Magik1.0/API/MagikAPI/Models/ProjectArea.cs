using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MagikAPI.Models
{
    public class ProjectArea
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        public int IconId { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public IEnumerable<Project> Projects { get; set; }
    }
}
