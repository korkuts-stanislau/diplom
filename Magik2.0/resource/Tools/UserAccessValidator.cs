using Microsoft.EntityFrameworkCore;
using Resource.Data;
using Resource.Data.Interfaces;

namespace Resource.Tools;

public class UserAccessValidator {
    private readonly IUnitOfWork uof;

    /// <summary>
    /// Validate access of user to data in DB
    /// </summary>
    /// <param name="uof">Data storage</param>
    public UserAccessValidator(IUnitOfWork uof)
    {
        this.uof = uof;
    }

    /// <summary>
    /// Validate if user owns project area
    /// </summary>
    /// <param name="accountId">User accoutn ID</param>
    /// <param name="areaId">Project area ID</param>
    /// <returns>Project area if user owner of that area</returns>
    public async Task<Models.ProjectArea> ValidateAndGetProjectAreaAsync(string accountId, int areaId) {
        var area = await uof.ProjectAreas.FirstOrDefaultAsync(areaId);
        if(area == null) throw new Exception("Нет такой области проектов");
        if(area.AccountId != accountId) throw new Exception("Эта область проектов принадлежит другому пользователю");
        return area;
    }

    /// <summary>
    /// Validate if user owns project
    /// </summary>
    /// <param name="accountId">User account ID</param>
    /// <param name="projectId">Project ID</param>
    /// <returns>Project if user owner of this project</returns>
    public async Task<Models.Project> ValidateAndGetProjectAsync(string accountId, int projectId) {
        var project = await uof.Projects.FirstOrDefaultAsync(projectId);
        if(project == null) throw new Exception("Нет такого проекта");
        await ValidateAndGetProjectAreaAsync(accountId, project.ProjectAreaId);
        return project;
    }
}