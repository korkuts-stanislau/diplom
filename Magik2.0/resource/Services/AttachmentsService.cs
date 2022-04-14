using AutoMapper;
using Resource.Data.Interfaces;
using Resource.Models;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class AttachmentsService
{
    private readonly IUnitOfWork uof;
    private readonly UserAccessValidator accessValidator;
    private readonly IMapper mapper;

    public AttachmentsService(IUnitOfWork uof, UserAccessValidator accessValidator, IMapper mapper)
    {
        this.uof = uof;
        this.accessValidator = accessValidator;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<AttachmentUI>> GetAccountAttachments(string accountId) {
        return mapper.Map<IEnumerable<AttachmentUI>>(await uof.AccountAttachments.GetAsync(accountId));
    }

    public async Task<IEnumerable<AttachmentUI>> GetStageAttachments(string accountId, int stageId) {
        var stagesAttachments = await uof.StagesAttachments.GetAsync(stageId);
        var attachments = stagesAttachments.Select(s => s.AccountAttachment);
        if(attachments.Any(a => a?.AccountId != accountId)) throw new ApplicationException("Попытка получения чужого вложения");
        return mapper.Map<IEnumerable<AttachmentUI>>(attachments);
    }

    public async Task CreateNewAttachmentAsync(string accountId, AttachmentUI attachment) {
        AccountAttachment attach = new AccountAttachment {
            Name = attachment.Name,
            AccountId = accountId,
            AttachmentTypeId = attachment.AttachmentTypeId,
            Data = attachment.Data
        };
        await uof.AccountAttachments.CreateAsync(attach);
        attachment.Id = attach.Id;
    }

    public async Task AddAttachmentToStageAsync(string accountId, int stageId, int attachmentId) {
        await accessValidator.ValidateAndGetStageAsync(accountId, stageId);
        await accessValidator.ValidateAndGetAttachmentAsync(accountId, attachmentId);
        var same = await uof.StagesAttachments.WhereAsync(a => a.StageId == stageId && a.AccountAttachmentId == attachmentId);
        if(same.Count() > 0) throw new ApplicationException("Это вложение уже находится в этой стадии");
        StageAttachment attach = new StageAttachment {
            StageId = stageId,
            AccountAttachmentId = attachmentId
        };
        await uof.StagesAttachments.CreateAsync(attach);
    }

    public async Task UpdateAttachmentAsync(string accountId, AttachmentUI attachment) {
        ArgumentNullException.ThrowIfNull(attachment.Id);
        var attach = await accessValidator.ValidateAndGetAttachmentAsync(accountId, (int)attachment.Id);
        attach.Name = attachment.Name;
        attach.Data = attachment.Data;
        await uof.AccountAttachments.UpdateAsync(attach);
    }

    public async Task DeleteAttachmentAsync(string accountId, int attachmentId) {
        ArgumentNullException.ThrowIfNull(attachmentId);
        var attach = await accessValidator.ValidateAndGetAttachmentAsync(accountId, attachmentId);
        await uof.AccountAttachments.DeleteAsync(attach);
    }
}