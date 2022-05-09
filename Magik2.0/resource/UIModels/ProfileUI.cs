using System.ComponentModel.DataAnnotations;

namespace Resource.UIModels;

public class ProfileUI
{
    public int Id { get; set;}
    
    [Required]
    [StringLength(50)]
    public string Username { get; set; } = null!;
    public string? Icon { get; set; } = null!;
    public string? Picture { get; set; } = null!;
    [Required]
    [StringLength(250)]
    public string Description { get; set; } = null!;
}