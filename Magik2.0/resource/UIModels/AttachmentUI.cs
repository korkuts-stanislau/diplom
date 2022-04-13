using System.ComponentModel.DataAnnotations;

namespace Resource.UIModels;

public class AttachmentUI
{
    public int? Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    //1 - Link
    //2 - Table
    public int AttachmentTypeId { get; set; }

    public string Data { get; set; } = null!;
}