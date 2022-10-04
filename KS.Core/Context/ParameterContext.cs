using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace KS.Core.Context;

internal class ParameterContext  : DbContext
{
    public DbSet<V1.Model.ParameterModel> Parameters { get; set; }
    
    public string DbPath { get; }
    
    public ParameterContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = System.IO.Path.Combine(Environment.GetFolderPath(folder),"content");
        DbPath = System.IO.Path.Join(path, "parameters.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(DbPath, options =>
        {
            options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        });
        base.OnConfiguring(optionsBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map table names
        modelBuilder.Entity<V1.Model.ParameterModel>().ToTable("Parameter", "Kurumin");
        modelBuilder.Entity<V1.Model.ParameterModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UpdateTime).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
        base.OnModelCreating(modelBuilder);
    }

}