using Microsoft.EntityFrameworkCore;
using Resource.Data;
using Resource.Data.Interfaces;

namespace Resource.Tools;

public class UserAccessValidator {
    private readonly AppDbContext context;

    public UserAccessValidator(AppDbContext context)
    {
        this.context = context;
    }
    public async Task<Models.ProjectArea> ValidateAndGetProjectAreaAsync(int areaId, string accountId) {
        var area = await context.ProjectAreas.FirstOrDefaultAsync(a => a.Id == areaId);
        if(area == null) throw new Exception("Нет такой области проектов");
        if(area.AccountId != accountId) throw new Exception("Эта область проектов принадлежит другому пользователю");
        return area;
    }
}