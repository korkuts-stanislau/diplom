using AutoMapper;
using Resource.Data.Interfaces;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class StagesService {
    private readonly IUnitOfWork uof;
    private readonly UserAccessValidator accessValidator;
    private readonly IMapper mapper;

    public StagesService(IUnitOfWork uof, UserAccessValidator accessValidator, IMapper mapper)
    {
        this.uof = uof;
        this.accessValidator = accessValidator;
        this.mapper = mapper;
    }

    public async Task CreateStageAsync(string accountId, int projectId, StageUI stage) {
        await accessValidator.ValidateAndGetProjectAsync(accountId, projectId);
        stage.CreationDate = DateTime.Now;
        stage.Progress = 0;
        Models.Stage newStage = new Models.Stage {
            ProjectId = projectId,
            Name = stage.Name,
            Description = stage.Description,
            CreationDate = (DateTime)stage.CreationDate,
            Deadline = stage.Deadline,
            Progress = (int)stage.Progress
        };
        await uof.Stages.CreateAsync(newStage);
        stage.Id = newStage.Id;
        stage.Color = ColorEvaluator.GetStageColor(newStage);
    }

    public async Task<IEnumerable<StageUI>> GetStagesAsync(string accountId, int projectId) {
        await accessValidator.ValidateAndGetProjectAsync(accountId, projectId);
        return mapper.Map<IEnumerable<StageUI>>(await uof.Stages.GetAsync(projectId));
    }

    public async Task UpdateStageAsync(string accountId, StageUI stage) {
        var stageToEdit = await accessValidator.ValidateAndGetStageAsync(accountId, stage.Id);
        stageToEdit.Name = stage.Name;
        stageToEdit.Description = stage.Description;
        stageToEdit.Deadline = stage.Deadline;
        stageToEdit.Progress = stage.Progress ?? 0;
        await uof.Stages.UpdateAsync(stageToEdit);
        stage.Color = ColorEvaluator.GetStageColor(stageToEdit);
    }

    public async Task DeleteStageAsync(string accountId, int stageId) {
        var stageToDelete = await accessValidator.ValidateAndGetStageAsync(accountId, stageId);
        await uof.Stages.DeleteAsync(stageToDelete);
    }
}