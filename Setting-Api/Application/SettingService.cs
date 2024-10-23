using Microsoft.EntityFrameworkCore;
using Setting_Api.Data.Context;
using Setting_Api.Model.Entity;

namespace Setting_Api.Application;


public interface ISettingService
{
    public Task<List<Setting>> GetSettings(int? casinoId);
    public Task Create(Setting setting);
    public Task Update(Setting setting);
    public Task UpdateGmtOffset();
}
public class SettingService(ApplicationDbContext context,IExternalService externalService) : ISettingService
{
    public async Task<List<Setting>> GetSettings(int? casinoId)
    {
        var query = context.Settings.AsQueryable();
        if (casinoId.HasValue)
        {
            query = query.Where(x => x.CasinoId == casinoId);
        }
        var settings = await query.ToListAsync();
        return settings;
    }
    public Task Create(Setting setting)
    {
        context.Settings.Add(setting);
        return context.SaveChangesAsync();
    }

    public Task Update(Setting setting)
    {
        context.Settings.Update(setting);
        return context.SaveChangesAsync();
    }

    public async Task UpdateGmtOffset()
    {
        var timeZone = await externalService.GetTimeZone();
        if (timeZone == 0) 
            return;
        var settings = await context.Settings.ToListAsync();
        
        foreach (var setting in settings)
        {
            setting.TimeZone = timeZone;
        }
        await context.SaveChangesAsync();
    }
}