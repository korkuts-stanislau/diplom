using System.ComponentModel.DataAnnotations;

namespace Resource.UIModels;

public class FieldUI
{
    public int Id { get; set; }
    [StringLength(50)]
    [Required]
    public string Name { get; set; } = null!;
    public string Icon { get; set; } = null!;
}