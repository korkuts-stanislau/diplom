using Microsoft.EntityFrameworkCore;
using Resource.Data;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class ProjectAreaService
{
    private readonly AppDbContext context;
    private readonly PictureConverter converter;

    public ProjectAreaService(AppDbContext context,
        PictureConverter converter)
    {
        this.context = context;
        this.converter = converter;
    }

    public async Task<int> CreateNewProjectArea(UIModels.ProjectArea area, string accountId) {
        Models.ProjectArea newArea = new Models.ProjectArea {
            Name = area.Name,
            AccountId = accountId,
            Icon = string.IsNullOrEmpty(area.Icon) ? new byte[0] : converter.RestrictImage(Convert.FromBase64String(area.Icon), 128, 128)            
        };
        await context.ProjectAreas.AddAsync(newArea);
        await context.SaveChangesAsync();
        return newArea.Id;
    }

    public async Task<IEnumerable<ProjectArea>> GetProjectAreas(string accountId) {
        return await context.ProjectAreas
            .Where(area => area.AccountId == accountId)
            .Select(area => new ProjectArea {
                Id = area.Id,
                Name = area.Name,
                Icon = area.Icon.Length != 0 ? Convert.ToBase64String(area.Icon) : ""
            })
            .ToListAsync();
    }

    public async Task EditProjectArea(UIModels.ProjectArea area, string accountId) {
        var areaToEdit = await ValidateAndGetProjectAreaAsync(area.Id, accountId);
        areaToEdit.Name = area.Name;
        if(!string.IsNullOrEmpty(area.Icon)) areaToEdit.Icon = converter.RestrictImage(Convert.FromBase64String(area.Icon), 128, 128);
        context.ProjectAreas.Update(areaToEdit);
        await context.SaveChangesAsync();
    }

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