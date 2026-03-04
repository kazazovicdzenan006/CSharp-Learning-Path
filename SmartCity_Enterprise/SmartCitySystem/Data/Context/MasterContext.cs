



using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.Identity;
using Domain.Identity;
public class MasterContext : IdentityDbContext<SystemCityUser, SystemCityRole, int>
{
    public MasterContext() { }
  

    public MasterContext(DbContextOptions<MasterContext> options) : base(options) { }

    public DbSet<Grad> Gradovi { get; set; }

    public DbSet<BibliotekaArtikal> Artikli { get; set; }

    public DbSet<CityNode> Nodes { get; set; }

    public DbSet<Uredjaj> Uredjaji { get; set; }


  


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Knjiga>().ToTable("Books");
        modelBuilder.Entity<Film>().ToTable("Movies");
        modelBuilder.Entity<ParkingLot>().ToTable("ParkingLots");
        modelBuilder.Entity<CrossRoad>().ToTable("CrossRoads");
        modelBuilder.Entity<Senzor>().ToTable("Senzors");
        modelBuilder.Entity<Kontroler>().ToTable("Controllers");




        modelBuilder.Entity<BibliotekaArtikal>()
            .HasOne(a => a.Grad)
            .WithMany(g => g.BookStoreInventory)
            .HasForeignKey(k => k.GradId);

        /* 
         Explainig realtions with fluent api 
        One baseClass object can be in just one City, but have multiple children classes inside that city
        and every object has Foreign Key GradId 
         */


        modelBuilder.Entity<Uredjaj>()
            .HasOne(a => a.Grad)
            .WithMany(g => g.Devices)
            .HasForeignKey(k => k.GradId);

        modelBuilder.Entity<CityNode>()
            .HasOne(a => a.Grad)
            .WithMany(g => g.CityNodes)
            .HasForeignKey(k => k.GradId);

        modelBuilder.Entity<Grad>()
            .HasKey(k => k.Id);
    }

}