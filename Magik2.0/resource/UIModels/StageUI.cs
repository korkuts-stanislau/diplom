using System.ComponentModel.DataAnnotations;

namespace Resource.UIModels;

public class StageUI {
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(2048)]
    public string Description { get; set; } = null!;

    public DateTime? CreationDate { get; set; }

    public DateTime Deadline { get; set; }

    [Range(0, 100)]
    public int? Progress { get; set; }
    public string? Color { get; set; }
}