using Infrastructure.Context.Configuration.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;
using Client = Support.Models.Client;

namespace Infrastructure.Context;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Client>().HasKey(c => c.Id);
        modelBuilder.Entity<Client>().Property(c => c.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Client>().Property(c => c.Email).IsRequired();
        modelBuilder.Entity<Client>().Property(c => c.Password).IsRequired();
        modelBuilder.Entity<Client>().Property(c => c.Firstname).IsRequired();
        modelBuilder.Entity<Client>().Property(c => c.Lastname).IsRequired();
        modelBuilder.Entity<Client>().Property(c => c.Phone).IsRequired();
        modelBuilder.Entity<Client>().Property(c => c.Type).IsRequired();
        modelBuilder.Entity<Client>().Property(c => c.Address).IsRequired();
        modelBuilder.Entity<Client>().Property(c => c.Company).IsRequired();
        modelBuilder.Entity<Client>().Property(c => c.Dni).IsRequired();
        modelBuilder.Entity<Client>().Property(c => c.Role).IsRequired();
        
        modelBuilder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}