namespace Resource.Models;

public class ProjectPartData
{
    public int Id { get; set; }

    public int ProjectPartId { get; set; }

    public int AccountFileId { get; set; }


    public ProjectPart ProjectPart { get; set; }
    
    public AccountFile AccountFile { get; set; }
}