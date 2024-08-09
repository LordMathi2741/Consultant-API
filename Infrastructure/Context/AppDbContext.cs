using Infrastructure.Context.Configuration.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;
using Support.Factory;
using Support.Factory.Company;
using Support.Factory.Cylinder;
using Support.Models;

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
        modelBuilder.Entity<User>().HasKey(c => c.Id);
        modelBuilder.Entity<User>().Property(c => c.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<User>().Property(c => c.Email).IsRequired();
        modelBuilder.Entity<User>().Property(c => c.Password).IsRequired();
        modelBuilder.Entity<User>().Property(c => c.Firstname).IsRequired();
        modelBuilder.Entity<User>().Property(c => c.Lastname).IsRequired();
        modelBuilder.Entity<User>().Property(c => c.Phone).IsRequired();
        modelBuilder.Entity<User>().Property(c => c.Address).IsRequired();
        modelBuilder.Entity<User>().Property(c => c.Company).IsRequired();
        modelBuilder.Entity<User>().Property(c => c.Dni).IsRequired();
        modelBuilder.Entity<User>().Property(c => c.Role).IsRequired();


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


        modelBuilder.Entity<WorkShopCompany>().HasKey(wc => wc.Id);
        modelBuilder.Entity<WorkShopCompany>().Property(wc => wc.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<WorkShopCompany>().Property(wc => wc.Address).IsRequired();
        modelBuilder.Entity<WorkShopCompany>().Property(wc => wc.Name).IsRequired();
        modelBuilder.Entity<WorkShopCompany>().Property(wc => wc.Ruc).IsRequired();
        modelBuilder.Entity<WorkShopCompany>().Property(wc => wc.Phone).IsRequired();

        modelBuilder.Entity<WorkShop>().HasKey(w => w.Id);
        modelBuilder.Entity<WorkShop>().Property(w => w.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<WorkShop>().Property(w => w.Address).IsRequired();
        modelBuilder.Entity<WorkShop>().Property(w => w.Name).IsRequired();
        modelBuilder.Entity<WorkShop>().Property(w => w.Phone).IsRequired();

        modelBuilder.Entity<WorkShopCylinder>().HasKey(wc => wc.Id);
        modelBuilder.Entity<WorkShopCylinder>().Property(wc => wc.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<WorkShopCylinder>().Property(wc => wc.Model).IsRequired();
        modelBuilder.Entity<WorkShopCylinder>().Property(wc => wc.Volume).IsRequired();
        modelBuilder.Entity<WorkShopCylinder>().Property(wc => wc.MadeDate).IsRequired();
        modelBuilder.Entity<WorkShopCylinder>().Property(wc => wc.SerialNumber).IsRequired();
        
        modelBuilder.Entity<ProviderCompany>().HasKey(pc => pc.Id);
        modelBuilder.Entity<ProviderCompany>().Property(pc => pc.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<ProviderCompany>().Property(pc => pc.Address).IsRequired();
        modelBuilder.Entity<ProviderCompany>().Property(pc => pc.Name).IsRequired();
        modelBuilder.Entity<ProviderCompany>().Property(pc => pc.Ruc).IsRequired();
        modelBuilder.Entity<ProviderCompany>().Property(pc => pc.Phone).IsRequired();
        modelBuilder.Entity<ProviderCompany>().Property(pc => pc.ContactPerson).IsRequired();
        

        modelBuilder.Entity<InstallerCompany>().HasKey(ic => ic.Id);
        modelBuilder.Entity<InstallerCompany>().Property(ic => ic.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<InstallerCompany>().Property(ic => ic.Address).IsRequired();
        modelBuilder.Entity<InstallerCompany>().Property(ic => ic.SocialReason).IsRequired();
        modelBuilder.Entity<InstallerCompany>().Property(ic => ic.Ruc).IsRequired();
        

        modelBuilder.Entity<CylinderProvider>().HasKey(cp => cp.Id);
        modelBuilder.Entity<CylinderProvider>().Property(cp => cp.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<CylinderProvider>().Property(cp => cp.SerieNumber).IsRequired();
        modelBuilder.Entity<CylinderProvider>().Property(cp => cp.Volume).IsRequired();
        modelBuilder.Entity<CylinderProvider>().Property(cp => cp.EmitDate).IsRequired();
        modelBuilder.Entity<CylinderProvider>().Property(cp => cp.Brand).IsRequired();
        
        

        modelBuilder.Entity<User>().HasMany<Cylinder>().WithOne().HasForeignKey(cy => cy.ClientId);
        modelBuilder.Entity<Cylinder>().HasOne<Valve>().WithOne().HasForeignKey<Valve>(v => v.CylinderId);
        modelBuilder.Entity<Owner>().HasMany<Vehicle>().WithOne().HasForeignKey(v => v.OwnerId);
        modelBuilder.Entity<User>().HasMany<OperationCenter>().WithOne().HasForeignKey(o => o.ClientId);
        modelBuilder.Entity<OperationCenter>().HasOne<Certifier>().WithOne()
            .HasForeignKey<Certifier>(v => v.OperationCenterId);
        modelBuilder.Entity<User>().HasMany<Observation>().WithOne().HasForeignKey(o => o.ClientId);
        
        
        modelBuilder.Entity<User>().HasMany<WorkShopCompany>().WithOne().HasForeignKey(wc => wc.UserId);
        modelBuilder.Entity<WorkShopCompany>().HasMany<WorkShop>().WithOne().HasForeignKey(w => w.WorkShopCompanyId);
        modelBuilder.Entity<WorkShop>().HasMany<WorkShopCylinder>().WithOne().HasForeignKey(wc => wc.WorkShopId);
        modelBuilder.Entity<WorkShop>().HasMany<InstallerCompany>().WithOne().HasForeignKey(ic => ic.WorkShopId);
        modelBuilder.Entity<User>().HasMany<ProviderCompany>().WithOne().HasForeignKey(pc => pc.UserId);
        modelBuilder.Entity<ProviderCompany>().HasMany<CylinderProvider>().WithOne().HasForeignKey(cp => cp.ProviderCompanyId);
        
        
        
        modelBuilder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}