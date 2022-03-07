namespace Resource.Data.Interfaces;

public interface IUnitOfWork {
    public IProfileRepository Profiles { get; }
    public IProjectAreaRepository ProjectAreas { get; }
    public IProjectRepository Projects { get; }
}