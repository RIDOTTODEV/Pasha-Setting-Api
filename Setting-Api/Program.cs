using Microsoft.EntityFrameworkCore;
using Setting_Api;
using Setting_Api.Application;
using Setting_Api.Data.Context;
using Setting_Api.Model.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //MySql
    options.UseMySql(builder.Configuration.GetConnectionString("Default"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default")));
});

//Services
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddHttpClient<IExternalService, ExternalService>(client =>
{
    client.BaseAddress = new Uri("http://api.timezonedb.com/v2.1/");
});

builder.Services.AddHostedService<Worker>();

var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// app.MapGet("/get-time-zone", async (IExternalService externalService, string zone="Europe/Athens") =>
// {
//     var timeZone = await externalService.GetTimeZone(zone);
//     return timeZone;
// });

app.MapGet("/get-settings", async (ISettingService settingService, int? casinoId) =>
{
    var settings = await settingService.GetSettings(casinoId);
    return settings;
});

app.MapPost("/create-setting", async (ISettingService settingService, Setting setting) =>
{
    await settingService.Create(setting);
    return Results.Created($"/create-setting", setting);
});

app.MapPut("/update-setting", async (ISettingService settingService, Setting setting) =>
{
    await settingService.Update(setting);
    return Results.Created($"/update-setting", setting);
});

app.MapPut("/update-gmt-offset", async (ISettingService settingService) =>
{
    await settingService.UpdateGmtOffset();
    return Results.Created($"/update-gmt-offset", "GMT Offset Updated");
});

await using var scope = app.Services.CreateAsyncScope();
await using var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await db.Database.MigrateAsync();
app.Run();