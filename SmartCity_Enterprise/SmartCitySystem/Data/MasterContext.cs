




using Microsoft.EntityFrameworkCore;
using Domain.Models; 
public class MasterContext : DbContext
{
    public DbSet<Grad> Gradovi { get; set; }

    public DbSet<BibliotekaArtikal> Artikli { get; set; }

    public DbSet<CityNode> Nodes { get; set; }

    public DbSet<Uredjaj> Uredjaji { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = SmartCity_EnterpriseDB; Trusted_Connection = True; TrustServerCertificate = True;");

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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


    }

}