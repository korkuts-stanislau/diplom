using Microsoft.EntityFrameworkCore;
using Resource.Data;
using Resource.Data.Interfaces;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class ProjectAreaService
{
    private readonly IProjectAreaRepository rep;
    private readonly PictureConverter converter;
    private readonly UserAccessValidator accessValidator;

    /// <summary>
    /// Service for project areas management
    /// </summary>
    /// <param name="rep">Project area repository</param>
    /// <param name="converter">Pictures converter</param>
    public ProjectAreaService(IProjectAreaRepository rep, PictureConverter converter, UserAccessValidator accessValidator)
    {
        this.rep = rep;
        this.converter = converter;
        this.accessValidator = accessValidator;
    }

    /// <summary>
    /// Create project area for account
    /// </summary>
    /// <param name="area">Project area to create</param>
    /// <param name="accountId">Account ID</param>
    /// <returns>Created project area ID</returns>
    public async Task<int> CreateProjectAreaAsync(UIModels.ProjectAreaUI area, string accountId) {
        Models.ProjectArea newArea = new Models.ProjectArea {
            Name = area.Name,
            AccountId = accountId,
            Icon = string.IsNullOrEmpty(area.Icon) ? null : converter.RestrictImage(Convert.FromBase64String(area.Icon), 128, 128)            
        };
        await rep.CreateAsync(newArea);
        return newArea.Id;
    }

    /// <summary>
    /// Get all account project areas
    /// </summary>
    /// <param name="accountId">Account ID</param>
    /// <returns>List of project areas of passed account</returns>
    public async Task<IEnumerable<ProjectAreaUI>> GetProjectAreasAsync(string accountId) {
        return (await rep.GetAsync(accountId))
            .Select(area => new ProjectAreaUI {
                Id = area.Id,
                Name = area.Name,
                Icon = area.Icon != null ? Convert.ToBase64String(area.Icon) : ""
            });
    }

    /// <summary>
    /// Update project area for account
    /// </summary>
    /// <param name="area">Update project area info</param>
    /// <param name="accountId">Account ID</param>
    public async Task UpdateProjectAreaAsync(UIModels.ProjectAreaUI area, string accountId) {
        var areaToEdit = await accessValidator.ValidateAndGetProjectAreaAsync(area.Id, accountId, rep);
        areaToEdit.Name = area.Name;
        if(!string.IsNullOrEmpty(area.Icon)) areaToEdit.Icon = converter.RestrictImage(Convert.FromBase64String(area.Icon), 128, 128);
        await rep.UpdateAsync(areaToEdit);
    }

    /// <summary>
    /// Delete account project area
    /// </summary>
    /// <param name="areaId">Project area ID</param>
    /// <param name="accountId">Account ID</param>
    public async Task DeleteProjectAreaAsync(int areaId, string accountId) {
        var area = await accessValidator.ValidateAndGetProjectAreaAsync(areaId, accountId, rep);
        await rep.DeleteAsync(area);
    }  
}