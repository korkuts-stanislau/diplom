using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Resource.Data;
using Resource.Data.Interfaces;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class ProjectAreaService
{
    private readonly IUnitOfWork uof;
    private readonly PictureConverter converter;
    private readonly UserAccessValidator accessValidator;
    private readonly IMapper mapper;

    public ProjectAreaService(IUnitOfWork uof, PictureConverter converter, UserAccessValidator accessValidator, IMapper mapper)
    {
        this.uof = uof;
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
        await uof.ProjectAreas.CreateAsync(newArea);
        area.Id = newArea.Id;
    }

    public async Task<IEnumerable<ProjectAreaUI>> GetProjectAreasAsync(string accountId) {
        return mapper.Map<IEnumerable<ProjectAreaUI>>(await uof.ProjectAreas.GetAsync(accountId));
    }

    public async Task UpdateProjectAreaAsync(string accountId, UIModels.ProjectAreaUI area) {
        var areaToEdit = await accessValidator.ValidateAndGetProjectAreaAsync(accountId, area.Id);
        areaToEdit.Name = area.Name;
        if(!string.IsNullOrEmpty(area.Icon)) areaToEdit.Icon = converter.RestrictImage(Convert.FromBase64String(area.Icon), 128, 128);
        await uof.ProjectAreas.UpdateAsync(areaToEdit);
    }

    public async Task DeleteProjectAreaAsync(string accountId, int areaId) {
        var area = await accessValidator.ValidateAndGetProjectAreaAsync(accountId, areaId);
        await uof.ProjectAreas.DeleteAsync(area);
    }  
}