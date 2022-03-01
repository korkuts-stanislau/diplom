using System.ComponentModel.DataAnnotations;

namespace Resource.Models;

public class AccountFile
{
    public int Id { get; set; }

    [StringLength(24)]
    public string AccountId { get; set; } = null!;

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int FileTypeId { get; set; }

    public byte[]? Data { get; set; }


    public FileType? FileType { get; set; }
}