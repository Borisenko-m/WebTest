using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;


class DBSets : DbContext
{
    public DbSet<CompanyHasClassifier> CompaniesHasClassifiers { get; set; }
    public DbSet<CompanyHasAddress> CompaniesHasAddresses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Classifier> Classifiers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<MicroDistrict> MicroDistricts { get; set; }
    public DbSet<Street> Streets { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<BuildingType> BuildingTypes { get; set; }
    public DBSets()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=localhost;UserId=root;Password=qweasd123;database=Testx247;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyHasAddress>().HasKey(c => new { c.CompanyID, c.AddressID });
        modelBuilder.Entity<CompanyHasClassifier>().HasKey(c => new { c.CompanyID, c.ClassifierID });
        //modelBuilder.Entity<Application>().Property(a => a.UserID).HasDefaultValue((ulong)0);
        modelBuilder.Entity<User>().Property(u => u.FirstName).HasDefaultValue("");
        modelBuilder.Entity<User>().Property(u => u.MiddleName).HasDefaultValue("");
        modelBuilder.Entity<User>().Property(u => u.LastName).HasDefaultValue("");
        modelBuilder.Entity<User>().Property(u => u.PhoneNumber).HasDefaultValue("");
    }
    ~DBSets()
    {
        Dispose();
    }
}