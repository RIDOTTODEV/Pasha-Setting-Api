using Setting_Api.Application;

namespace Setting_Api;

public class Worker(IServiceScopeFactory scopeFactory, ILogger<Worker> logger)
    : BackgroundService
{
    private Timer? timer; 

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        
        var now = DateTime.Now;
        var nextRunTime = now.Date.AddDays(1).AddHours(5); 
        
        var firstDelay = nextRunTime - now; 
        if (firstDelay < TimeSpan.Zero) 
        {
            firstDelay = firstDelay.Add(TimeSpan.FromDays(1));
        }

        timer = new Timer(async _ =>
        {
            await Work(stoppingToken);
        }, null, firstDelay, TimeSpan.FromDays(1));

        return Task.CompletedTask; 
    }

    private async Task Work(CancellationToken stoppingToken)
    {
       
        stoppingToken.ThrowIfCancellationRequested();

        logger.LogInformation("Worker executing at: {time}", DateTimeOffset.Now);

        using var scope = scopeFactory.CreateScope();
        var settingService = scope.ServiceProvider.GetRequiredService<ISettingService>();
        var externalService = scope.ServiceProvider.GetRequiredService<IExternalService>();
        
        var settings = await settingService.GetSettings(null);
        var settingZone = settings.FirstOrDefault(x => x.TimeZone != null);
        
        var newZone = await externalService.GetTimeZone();
        
        if (settingZone != null && settingZone.TimeZone != newZone)
        {
            await settingService.UpdateGmtOffset();
        }
    }

    public override Task StopAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Worker stopping.");
        
        timer?.Change(Timeout.Infinite, 0);
        
        return base.StopAsync(stoppingToken);
    }
}
