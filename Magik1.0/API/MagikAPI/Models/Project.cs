using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MagikAPI.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }
        public int ProjectAreaId { get; set; }
        public ProjectArea ProjectArea { get; set; }
        public IEnumerable<ProjectPart> ProjectParts { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"{Name}\n" +
                $"{Description}\n" +
                $"Части проекта:\n");

            foreach (var part in ProjectParts)
            {
                builder.Append(part.ToString() + Environment.NewLine);
            }

            return builder.ToString();
        }
    }
}
