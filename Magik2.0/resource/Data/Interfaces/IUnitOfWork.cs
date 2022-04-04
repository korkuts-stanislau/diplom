namespace Resource.Data.Interfaces;

public interface IUnitOfWork {
    public IProfilesRepository Profiles { get; }
    public IFieldsRepository Fields { get; }
    public IProjectsRepository Projects { get; }
    public IStagesRepository Stages { get; }
}