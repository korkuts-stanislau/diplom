using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Resource.Data;
using Resource.Data.Interfaces;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class FieldsService
{
    private readonly IUnitOfWork uof;
    private readonly PictureConverter converter;
    private readonly UserAccessValidator accessValidator;
    private readonly IMapper mapper;

    public FieldsService(IUnitOfWork uof, PictureConverter converter, UserAccessValidator accessValidator, IMapper mapper)
    {
        this.uof = uof;
        this.converter = converter;
        this.accessValidator = accessValidator;
        this.mapper = mapper;
    }

    public async Task CreateFieldAsync(string accountId, UIModels.FieldUI field) {
        var icon = string.IsNullOrEmpty(field.Icon) ? null : converter.RestrictImage(Convert.FromBase64String(field.Icon), 128, 128); // create icon from user image
        Models.Field newField = new Models.Field {
            Name = field.Name,
            AccountId = accountId,
            Icon = icon
        };
        await uof.Fields.CreateAsync(newField);
        field.Id = newField.Id;
    }

    public async Task<IEnumerable<FieldUI>> GetFieldsAsync(string accountId) {
        return mapper.Map<IEnumerable<FieldUI>>(await uof.Fields.GetAsync(accountId));
    }

    public async Task UpdateFieldAsync(string accountId, UIModels.FieldUI field) {
        var fieldToEdit = await accessValidator.ValidateAndGetFieldAsync(accountId, field.Id);
        fieldToEdit.Name = field.Name;
        if(!string.IsNullOrEmpty(field.Icon)) fieldToEdit.Icon = converter.RestrictImage(Convert.FromBase64String(field.Icon), 128, 128);
        await uof.Fields.UpdateAsync(fieldToEdit);
    }

    public async Task DeleteFieldAsync(string accountId, int fieldId) {
        var field = await accessValidator.ValidateAndGetFieldAsync(accountId, fieldId);
        await uof.Fields.DeleteAsync(field);
    }  
}