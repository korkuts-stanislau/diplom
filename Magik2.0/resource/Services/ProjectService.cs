using Resource.Data.Interfaces;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class ProjectService {
    private readonly IProjectRepository rep;
    private readonly UserAccessValidator accessValidator;

    public ProjectService(IProjectRepository rep, UserAccessValidator accessValidator)
    {
        this.rep = rep;
        this.accessValidator = accessValidator;
    }

    public async Task<IEnumerable<ProjectUI>> GetProjectsAsync(int areaId, string accountId) {
        await accessValidator.ValidateAndGetProjectAreaAsync(areaId, accountId);
        throw new NotImplementedException();
    }

     
}