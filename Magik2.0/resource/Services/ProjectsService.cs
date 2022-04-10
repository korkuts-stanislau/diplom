using AutoMapper;
using Resource.Data.Interfaces;
using Resource.Models;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class ProjectsService {
    private readonly IUnitOfWork uof;
    private readonly UserAccessValidator accessValidator;
    private readonly IMapper mapper;

    public ProjectsService(IUnitOfWork uof, UserAccessValidator accessValidator, IMapper mapper)
    {
        this.uof = uof;
        this.accessValidator = accessValidator;
        this.mapper = mapper;
    }

    public async Task CreateProjectAsync(string accountId, int fieldId, ProjectUI project) {
        await accessValidator.ValidateAndGetFieldAsync(accountId, fieldId);
        project.ProjectTypeId = 1; //private project by default
        project.Color = Resource.MapperProfiles.ProjectMapperProfile.DEFAULT_COLOR;
        Project newProject = new Project {
            FieldId = fieldId,
            ProjectTypeId = project.ProjectTypeId,
            OriginalProjectId = null,
            Name = project.Name,
            Description = project.Description
        };
        await uof.Projects.CreateAsync(newProject);
        project.Id = newProject.Id;
    }

    public async Task<IEnumerable<ProjectUI>> GetProjectsAsync(string accountId, int fieldId) {
        await accessValidator.ValidateAndGetFieldAsync(accountId, fieldId);
        return mapper.Map<IEnumerable<ProjectUI>>(await uof.Projects.GetAsync(fieldId));
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