namespace Resource.Models;

public class StageFile
{
    public int Id { get; set; }

    public int StageId { get; set; }

    public int AccountFileId { get; set; }


    public Stage? Stage { get; set; }
    
    public AccountFile? AccountFile { get; set; }
}