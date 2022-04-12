using System.ComponentModel.DataAnnotations;

namespace Resource.Models;

public class AttachmentType
{
    public int Id { get; set; }
    
    [StringLength(20)]
    public string Name { get; set; } = null!;
}