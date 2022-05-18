using Resource.Data.Interfaces;
using Resource.Models;
using Resource.UIModels;

namespace Resource.Services;

public class StatisticService {
    private readonly IUnitOfWork uof;

    public StatisticService(IUnitOfWork uof)
    {
        this.uof = uof;
    }

    public async Task<StatisticUI> GenerateStatisticForUserAsync(string accountId) {
        var fields = await uof.Fields.GetAsync(accountId);
        var projects = new List<Project>();
        foreach(var field in fields) {
            projects.AddRange(await uof.Projects.GetAsync(field.Id));
        }
        var stages = new List<Stage>();
        foreach(var project in projects) {
            stages.AddRange(await uof.Stages.GetAsync(project.Id));
        }
        return new StatisticUI {
            FieldsQuantity = fields.Count(),
            ProjectsQuantity = projects.Count(),
            StagesQuantity = stages.Count(),
            CompletedStagesQuantity = stages.Count(s => s.Progress == 100)
        };
    }
}