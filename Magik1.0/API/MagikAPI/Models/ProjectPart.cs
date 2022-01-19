using System;
using System.ComponentModel.DataAnnotations;

namespace MagikAPI.Models
{
    public class ProjectPart
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        [MaxLength(2048)]
        public string Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeadLine { get; set; }
        [Range(0, 100)]
        public int? Progress { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public override string ToString()
        {
            return $"{Name}\n" +
                $"{Description}\n" +
                $"Создан {CreationDate.Value!.ToShortDateString()}\n" +
                $"Последний срок {DeadLine.Value!.ToShortDateString()}\n" +
                $"Прогресс выполнения {Progress.Value}";
        }
    }
}
