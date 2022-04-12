namespace Resource.Models;

public class StageAttachment
{
    public int Id { get; set; }

    public int StageId { get; set; }

    public int AccountAttachmentId { get; set; }


    public Stage? Stage { get; set; }
    
    public AccountAttachment? AccountAttachment { get; set; }
}