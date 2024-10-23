using Microsoft.EntityFrameworkCore;
using Setting_Api.Data.Mapping;

namespace Setting_Api.Data.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    
    public DbSet<Model.Entity.Setting> Settings { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.ApplyConfiguration(new SettingMapping());
    }
    
}