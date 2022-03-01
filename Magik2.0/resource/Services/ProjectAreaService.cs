using Microsoft.EntityFrameworkCore;
using Resource.Data;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class ProjectAreaService
{
    private readonly AppDbContext context;
    private readonly PictureConverter converter;

    /// <summary>
    /// Service for project areas management
    /// </summary>
    /// <param name="context">Data context</param>
    /// <param name="converter">Pictures converter</param>
    public ProjectAreaService(AppDbContext context,
        PictureConverter converter)
    {
        this.context = context;
        this.converter = converter;
    }

    /// <summary>
    /// Create project area for account
    /// </summary>
    /// <param name="area">Project area to create</param>
    /// <param name="accountId">Account ID</param>
    /// <returns>Created project area ID</returns>
    public async Task<int> CreateProjectArea(UIModels.ProjectArea area, string accountId) {
        Models.ProjectArea newArea = new Models.ProjectArea {
            Name = area.Name,
            AccountId = accountId,
            Icon = string.IsNullOrEmpty(area.Icon) ? new byte[0] : converter.RestrictImage(Convert.FromBase64String(area.Icon), 128, 128)            
        };
        await context.ProjectAreas.AddAsync(newArea);
        await context.SaveChangesAsync();
        return newArea.Id;
    }

    /// <summary>
    /// Get all account project areas
    /// </summary>
    /// <param name="accountId">Account ID</param>
    /// <returns>List of project areas of passed account</returns>
    public async Task<IEnumerable<ProjectArea>> GetProjectAreas(string accountId) {
        return await context.ProjectAreas
            .Where(area => area.AccountId == accountId)
            .Select(area => new ProjectArea {
                Id = area.Id,
                Name = area.Name,
                Icon = area.Icon != null ? Convert.ToBase64String(area.Icon) : ""
            })
            .ToListAsync();
    }

    /// <summary>
    /// Update project area for account
    /// </summary>
    /// <param name="area">Update project area info</param>
    /// <param name="accountId">Account ID</param>
    public async Task UpdateProjectArea(UIModels.ProjectArea area, string accountId) {
        var areaToEdit = await ValidateAndGetProjectAreaAsync(area.Id, accountId);
        areaToEdit.Name = area.Name;
        if(!string.IsNullOrEmpty(area.Icon)) areaToEdit.Icon = converter.RestrictImage(Convert.FromBase64String(area.Icon), 128, 128);
        context.ProjectAreas.Update(areaToEdit);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Delete account project area
    /// </summary>
    /// <param name="areaId">Project area ID</param>
    /// <param name="accountId">Account ID</param>
    public async Task DeleteProjectArea(int areaId, string accountId) {
        var area = await ValidateAndGetProjectAreaAsync(areaId, accountId);
        context.ProjectAreas.Remove(area);
        await context.SaveChangesAsync();
    }

    private async Task<Models.ProjectArea> ValidateAndGetProjectAreaAsync(int areaId, string accountId) {
        var area = await context.ProjectAreas.FirstOrDefaultAsync(a => a.Id == areaId);
        if(area == null) throw new Exception("Нет такой области проектов");
        if(area.AccountId != accountId) throw new Exception("Эта область проектов принадлежит другому пользователю");
        return area;
    }    
}