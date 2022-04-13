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
            AttachmentTypeId = attachment.AttachmentTypeId,
            Data = attachment.Data
        };
        await uof.AccountAttachments.CreateAsync(attach);
        attachment.Id = attach.Id;
    }

    public async Task UpdateAttachmentAsync(string accountId, AttachmentUI attachment) {
        ArgumentNullException.ThrowIfNull(attachment.Id);
        var attach = await accessValidator.ValidateAndGetAttachmentAsync(accountId, (int)attachment.Id);
        attach.Name = attachment.Name;
        attach.Data = attachment.Data;
        await uof.AccountAttachments.UpdateAsync(attach);
    }

    public async Task DeleteAttachmentAsync(string accountId, AttachmentUI attachment) {
        ArgumentNullException.ThrowIfNull(attachment.Id);
        var attach = await accessValidator.ValidateAndGetAttachmentAsync(accountId, (int)attachment.Id);
        await uof.AccountAttachments.DeleteAsync(attach);
    }
}