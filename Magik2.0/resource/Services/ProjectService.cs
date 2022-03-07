using AutoMapper;
using Resource.Data.Interfaces;
using Resource.Models;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class ProjectService {
    private readonly IUnitOfWork uof;
    private readonly UserAccessValidator accessValidator;
    private readonly IMapper mapper;

    public ProjectService(IUnitOfWork uof, UserAccessValidator accessValidator, IMapper mapper)
    {
        this.uof = uof;
        this.accessValidator = accessValidator;
        this.mapper = mapper;
    }

    public async Task CreateProjectAsync(string accountId, int projectAreaId, ProjectUI project) {
        await accessValidator.ValidateAndGetProjectAreaAsync(accountId, projectAreaId);
        Project newProject = new Project {
            ProjectAreaId = projectAreaId,
            Name = project.Name,
            Description = project.Description
        };
        await uof.Projects.CreateAsync(newProject);
        project.Id = newProject.Id;
    }

    public async Task<IEnumerable<ProjectUI>> GetProjectsAsync(string accountId, int areaId) {
        await accessValidator.ValidateAndGetProjectAreaAsync(accountId, areaId);
        return mapper.Map<IEnumerable<ProjectUI>>(await uof.Projects.GetAsync(areaId));
    }

    public async Task UpdateProjectAsync(string accountId, ProjectUI project) {
        var projectToEdit = await accessValidator.ValidateAndGetProjectAsync(accountId, project.Id);
        projectToEdit.Name = project.Name;
        projectToEdit.Description = project.Description;
        await uof.Projects.UpdateAsync(projectToEdit);
    }

    public async Task DeleteProjectAsync(string accountId, int projectId) {
        var project = await accessValidator.ValidateAndGetProjectAsync(accountId, projectId);
        await uof.Projects.DeleteAsync(project);
    }
}