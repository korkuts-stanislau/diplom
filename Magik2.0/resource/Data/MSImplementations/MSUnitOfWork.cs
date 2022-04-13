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
    private MSStagesRepository? stages;
    public IStagesRepository Stages => stages ??= new MSStagesRepository(context);
    private MSAccountAttachmentsRepository? accAttachments;
    public IAccountAttachmentsRepository AccountAttachments => accAttachments ??= new MSAccountAttachmentsRepository(context);
    private MSStagesAttachmentsRepository? stagesAttachments;
    public IStagesAttachmentsRepository StagesAttachments => stagesAttachments ??= new MSStagesAttachmentsRepository(context);
}