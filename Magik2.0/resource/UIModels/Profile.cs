using System.ComponentModel.DataAnnotations;

namespace Resource.UIModels;

public class Profile
{
    [Required]
    [StringLength(50)]
    public string Username { get; set; }
    public string Picture { get; set; }
    [Required]
    [StringLength(200)]
    public string Description { get; set; }
}