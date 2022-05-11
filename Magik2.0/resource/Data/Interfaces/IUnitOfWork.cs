namespace Resource.Data.Interfaces;

public interface IUnitOfWork {
    public IProfilesRepository Profiles { get; }
    public IFieldsRepository Fields { get; }
    public IProjectsRepository Projects { get; }
    public IStagesRepository Stages { get; }
    public IAccountAttachmentsRepository AccountAttachments{ get; }
    public IStagesAttachmentsRepository StagesAttachments { get; }
    public ICardsRepository Cards { get; }
}