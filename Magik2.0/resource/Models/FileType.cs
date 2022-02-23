using System.ComponentModel.DataAnnotations;

namespace Resource.Models;

public class FileType
{
    public int Id { get; set; }
    
    [StringLength(5)]
    public string Name { get; set; }
}