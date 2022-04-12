using System.ComponentModel.DataAnnotations;

namespace Resource.Models;

public class AccountAttachment
{
    public int Id { get; set; }

    [StringLength(24)]
    public string AccountId { get; set; } = null!;

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int AttachmentTypeId { get; set; }

    public string Data { get; set; } = null!;


    public AttachmentType? AttachmentType { get; set; }
}