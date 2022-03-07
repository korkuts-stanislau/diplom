using Resource.Data.Interfaces;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class ProjectService {
    private readonly IProjectRepository projectRep;
    private readonly IProjectAreaRepository areaRep;
    private readonly UserAccessValidator accessValidator;

    public ProjectService(IProjectRepository projectRep, IProjectAreaRepository areaRep, UserAccessValidator accessValidator)
    {
        this.projectRep = projectRep;
        this.areaRep = areaRep;
        this.accessValidator = accessValidator;
    }

    public async Task<IEnumerable<ProjectUI>> GetProjectsAsync(string accountId, int areaId) {
        await accessValidator.ValidateAndGetProjectAreaAsync(accountId, areaId, areaRep);
        throw new NotImplementedException();
    }

     
}