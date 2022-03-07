using AutoMapper;
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
    private readonly IMapper mapper;

    public ProjectAreaService(IProjectAreaRepository rep, PictureConverter converter, UserAccessValidator accessValidator, IMapper mapper)
    {
        this.rep = rep;
        this.converter = converter;
        this.accessValidator = accessValidator;
        this.mapper = mapper;
    }

    public async Task CreateProjectAreaAsync(string accountId, UIModels.ProjectAreaUI area) {
        var icon = string.IsNullOrEmpty(area.Icon) ? null : converter.RestrictImage(Convert.FromBase64String(area.Icon), 128, 128); // create icon from user image
        Models.ProjectArea newArea = new Models.ProjectArea {
            Name = area.Name,
            AccountId = accountId,
            Icon = icon
        };
        await rep.CreateAsync(newArea);
        area.Id = newArea.Id;
    }

    public async Task<IEnumerable<ProjectAreaUI>> GetProjectAreasAsync(string accountId) {
        return mapper.Map<IEnumerable<ProjectAreaUI>>(await rep.GetAsync(accountId));
    }

    public async Task UpdateProjectAreaAsync(string accountId, UIModels.ProjectAreaUI area) {
        var areaToEdit = await accessValidator.ValidateAndGetProjectAreaAsync(accountId, area.Id, rep);
        areaToEdit.Name = area.Name;
        if(!string.IsNullOrEmpty(area.Icon)) areaToEdit.Icon = converter.RestrictImage(Convert.FromBase64String(area.Icon), 128, 128);
        await rep.UpdateAsync(areaToEdit);
    }

    public async Task DeleteProjectAreaAsync(string accountId, int areaId) {
        var area = await accessValidator.ValidateAndGetProjectAreaAsync(accountId, areaId, rep);
        await rep.DeleteAsync(area);
    }  
}