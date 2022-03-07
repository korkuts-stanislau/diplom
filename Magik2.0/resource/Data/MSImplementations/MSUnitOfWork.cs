using Resource.Data.Interfaces;

namespace Resource.Data.MSImplementations;

public class MSUnitOfWork : IUnitOfWork {
    private readonly AppDbContext context;

    public MSUnitOfWork(AppDbContext context)
    {
        this.context = context;
    }

    private MSProfileRepository? profiles;
    public IProfileRepository Profiles => profiles ??= new MSProfileRepository(context);
    private MSProjectAreaRepository? projectAreas;
    public IProjectAreaRepository ProjectAreas => projectAreas ??= new MSProjectAreaRepository(context);
    private MSProjectRepository? projects;
    public IProjectRepository Projects => projects ??= new MSProjectRepository(context);
}