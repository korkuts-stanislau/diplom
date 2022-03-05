using Resource.Data.Interfaces;
using Resource.UIModels;

namespace Resource.Services;

public class ProjectService {
    private readonly IProjectRepository rep;

    public ProjectService(IProjectRepository rep)
    {
        this.rep = rep;
    }

    public async Task<IEnumerable<ProjectUI>> GetProjectsAsync(int projectAreaId) {
        throw new NotImplementedException();
    }

     
}