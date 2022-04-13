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
    /// Validate if user owns field
    /// </summary>
    /// <param name="accountId">User accoutn ID</param>
    /// <param name="fieldId">Field ID</param>
    /// <returns>Field if user owner of that field</returns>
    public async Task<Models.Field> ValidateAndGetFieldAsync(string accountId, int fieldId) {
        var field = await uof.Fields.FirstOrDefaultAsync(fieldId);
        if(field == null) throw new ApplicationException("Нет такой области проектов");
        if(field.AccountId != accountId) throw new ApplicationException("Эта область проектов принадлежит другому пользователю");
        return field;
    }

    /// <summary>
    /// Validate if user owns project
    /// </summary>
    /// <param name="accountId">User account ID</param>
    /// <param name="projectId">Project ID</param>
    /// <returns>Project if user owner of this project</returns>
    public async Task<Models.Project> ValidateAndGetProjectAsync(string accountId, int projectId) {
        var project = await uof.Projects.FirstOrDefaultAsync(projectId);
        if(project == null) throw new ApplicationException("Нет такого проекта");
        await ValidateAndGetFieldAsync(accountId, project.FieldId);
        return project;
    }

    public async Task<Models.Stage> ValidateAndGetStageAsync(string accountId, int stageId) {
        var stage = await uof.Stages.FirstOrDefaultAsync(stageId);
        if(stage == null) throw new ApplicationException("Нет такого этапа");
        await ValidateAndGetProjectAsync(accountId, stage.ProjectId);
        return stage;
    }

    public async Task<Models.AccountAttachment> ValidateAndGetAttachmentAsync(string accountId, int attachmentId) {
        var attachment = await uof.AccountAttachments.FirstOrDefaultAsync(attachmentId);
        if(attachment == null) throw new ApplicationException("Нет такого вложения");
        if(attachment.AccountId != accountId) throw new ApplicationException("Это вложение принадлежит другому пользователю");
        return attachment;
    }
}