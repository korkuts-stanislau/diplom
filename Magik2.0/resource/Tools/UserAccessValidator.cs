using Resource.Data.Interfaces;

namespace Resource.Tools;

public class UserAccessValidator {
    public async Task<Models.ProjectArea> ValidateAndGetProjectAreaAsync(int areaId, string accountId, IProjectAreaRepository repository) {
        var area = await repository.FirstOrDefaultAsync(areaId);
        if(area == null) throw new Exception("Нет такой области проектов");
        if(area.AccountId != accountId) throw new Exception("Эта область проектов принадлежит другому пользователю");
        return area;
    }  
}