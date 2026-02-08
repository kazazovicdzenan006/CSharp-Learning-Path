
using Microsoft.EntityFrameworkCore;
public class SenzorsContext : DbContext
{
    public DbSet<Uredjaj> Devices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Uredjaj>().ToTable("Devices");
        modelBuilder.Entity<Senzor>().ToTable("Senzors");
        modelBuilder.Entity<Kontroler>().ToTable("Controllers"); 
    }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = SenzorsDatabase; Trusted_Connection = True;"); 
    }





}



