using Resource.Data.Interfaces;

namespace Resource.Data.MSImplementations;

public class MSUnitOfWork : IUnitOfWork {
    private readonly AppDbContext context;

    public MSUnitOfWork(AppDbContext context)
    {
        this.context = context;
    }

    private MSProfilesRepository? profiles;
    public IProfilesRepository Profiles => profiles ??= new MSProfilesRepository(context);
    private MSFieldsRepository? fields;
    public IFieldsRepository Fields => fields ??= new MSFieldsRepository(context);
    private MSProjectsRepository? projects;
    public IProjectsRepository Projects => projects ??= new MSProjectsRepository(context);
}