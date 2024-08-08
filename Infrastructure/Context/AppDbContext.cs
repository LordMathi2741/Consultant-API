using Infrastructure.Context.Configuration.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;
using Support.Models;
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
        modelBuilder.Entity<Client>().Property(c => c.Address).IsRequired();
        modelBuilder.Entity<Client>().Property(c => c.Company).IsRequired();
        modelBuilder.Entity<Client>().Property(c => c.Dni).IsRequired();
        modelBuilder.Entity<Client>().Property(c => c.Role).IsRequired();


        modelBuilder.Entity<Vehicle>().HasKey(v => v.Id);
        modelBuilder.Entity<Vehicle>().Property(v => v.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Vehicle>().Property(v => v.VehicleIdentifier).IsRequired();
        
        modelBuilder.Entity<Cylinder>().HasKey(c => c.Id);
        modelBuilder.Entity<Cylinder>().Property(c => c.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Cylinder>().Property(c => c.Brand).IsRequired();
        modelBuilder.Entity<Cylinder>().Property(c => c.Capacity).IsRequired();
        modelBuilder.Entity<Cylinder>().Property(c => c.SerieNumber).IsRequired();
        
        modelBuilder.Entity<Valve>().HasKey(v => v.Id);
        modelBuilder.Entity<Valve>().Property(v => v.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Valve>().Property(v => v.SerieNumber).IsRequired();
        modelBuilder.Entity<Valve>().Property(v => v.Brand).IsRequired();
        modelBuilder.Entity<Valve>().Property(v => v.Model).IsRequired();
        
        
        modelBuilder.Entity<Owner>().HasKey(o => o.Id);
        modelBuilder.Entity<Owner>().Property(o => o.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Owner>().Property(o => o.FirstName).IsRequired();
        modelBuilder.Entity<Owner>().Property(o => o.LastName).IsRequired();
        modelBuilder.Entity<Owner>().Property(o => o.Dni).IsRequired();
        modelBuilder.Entity<Owner>().Property(o => o.Phone).IsRequired();
        modelBuilder.Entity<Owner>().Property(o => o.Address).IsRequired();
        modelBuilder.Entity<Owner>().Property(o => o.Province).IsRequired();
        modelBuilder.Entity<Owner>().Property(o => o.District).IsRequired();
        modelBuilder.Entity<Owner>().Property(o => o.Department).IsRequired();
        
        modelBuilder.Entity<Certifier>().HasKey(c => c.Id);
        modelBuilder.Entity<Certifier>().Property(c => c.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Certifier>().Property(c => c.Brand).IsRequired();
        modelBuilder.Entity<Certifier>().Property(c => c.Name).IsRequired();
        
        modelBuilder.Entity<OperationCenter>().HasKey(op => op.Id);
        modelBuilder.Entity<OperationCenter>().Property(op => op.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<OperationCenter>().Property(op => op.Name).IsRequired();
        modelBuilder.Entity<OperationCenter>().Property(op => op.Address).IsRequired();
        modelBuilder.Entity<OperationCenter>().Property(op => op.Phone).IsRequired();

        modelBuilder.Entity<Observation>().HasKey(ob => ob.Id);
        modelBuilder.Entity<Observation>().Property(ob => ob.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Observation>().Property(ob => ob.Content);

        modelBuilder.Entity<Vehicle>().HasMany<Cylinder>().WithOne().HasForeignKey(c => c.VehicleId);
        modelBuilder.Entity<Valve>().HasOne<Cylinder>().WithMany().HasForeignKey(v => v.CylinderId);
        modelBuilder.Entity<Owner>().HasMany<Vehicle>().WithOne().HasForeignKey(o => o.OwnerId);
        modelBuilder.Entity<Owner>().HasOne<OperationCenter>().WithMany().HasForeignKey(o => o.OperationCenterId);
        modelBuilder.Entity<Client>().HasMany<Observation>().WithOne().HasForeignKey(o => o.ClientId);
        
        
        
        modelBuilder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}