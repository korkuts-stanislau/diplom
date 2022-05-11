using System.ComponentModel.DataAnnotations;

namespace Resource.UIModels
{
    public class CardUI
    {
        public int Id { get; set; }

        [StringLength(1024)]
        public string Question { get; set; } = null!;

        [StringLength(1024)]
        public string Answer { get; set; } = null!;
    }
}
