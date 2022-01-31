namespace Resource.Models;

public class ProjectData
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int AccountFileId { get; set; }

    public Project Project { get; set; }
    public AccountFile AccountFile { get; set; }
}