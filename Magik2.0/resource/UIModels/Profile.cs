using System.ComponentModel.DataAnnotations;

namespace Resource.UIModels;

public class Profile
{
    [Required]
    [StringLength(50)]
    public string Username { get; set; } = null!;
    public string Picture { get; set; } = null!;
    [Required]
    [StringLength(200)]
    public string Description { get; set; } = null!;
}