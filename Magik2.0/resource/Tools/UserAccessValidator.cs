using Microsoft.EntityFrameworkCore;
using Resource.Data;
using Resource.Data.Interfaces;

namespace Resource.Tools;

public class UserAccessValidator {
    public async Task<Models.ProjectArea> ValidateAndGetProjectAreaAsync(string accountId, int areaId, IProjectAreaRepository rep) {
        var area = await rep.FirstOrDefaultAsync(areaId);
        if(area == null) throw new Exception("Нет такой области проектов");
        if(area.AccountId != accountId) throw new Exception("Эта область проектов принадлежит другому пользователю");
        return area;
    }
}