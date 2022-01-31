using System.ComponentModel.DataAnnotations;

namespace Resource.Models;

public class AccountFile
{
    public int Id { get; set; }
    [StringLength(24)]
    public string AccountId { get; set; }
    [StringLength(32)]
    public string Name { get; set; }
    public int FileTypeId { get; set; }
    public byte[] Data { get; set; }

    public FileType FileType { get; set; }
}