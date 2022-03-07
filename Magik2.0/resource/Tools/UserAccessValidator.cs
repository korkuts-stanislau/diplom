using Microsoft.EntityFrameworkCore;
using Resource.Data;
using Resource.Data.Interfaces;

namespace Resource.Tools;

public class UserAccessValidator {
    private readonly IUnitOfWork uof;

    public UserAccessValidator(IUnitOfWork uof)
    {
        this.uof = uof;
    }
    public async Task<Models.ProjectArea> ValidateAndGetProjectAreaAsync(string accountId, int areaId) {
        var area = await uof.ProjectAreas.FirstOrDefaultAsync(areaId);
        if(area == null) throw new Exception("Нет такой области проектов");
        if(area.AccountId != accountId) throw new Exception("Эта область проектов принадлежит другому пользователю");
        return area;
    }
}