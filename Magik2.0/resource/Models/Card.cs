using System.ComponentModel.DataAnnotations;

namespace Resource.Models
{
    public class Card
    {
        public int Id { get; set; }

        [StringLength(1024)]
        public string Question { get; set; } = null!;

        [StringLength(1024)]
        public string Answer { get; set; } = null!;
        
        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;
    }
}
