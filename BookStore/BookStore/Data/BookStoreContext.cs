

using Microsoft.EntityFrameworkCore;

public class BookStoreContext : DbContext
{
  public DbSet<BibliotekaArtikal> StoreItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Table per Hierarchy (TPH) is default ef core option (one big table with Discriminator for recognising objects)
        // but in that case we don't have normalized database so I use
        // Table per type (TPT) so I can have normalized database. In this scenario we have to create table for every object
        // and ef core can't just use select, it has to use select with join so it can show all data
        // It is little bit more complex to implement but better for bigger solutions because of normalization and avoiding null fields in tables
       

        modelBuilder.Entity<BibliotekaArtikal>().ToTable("BookStoreItems");
        modelBuilder.Entity<Knjiga>().ToTable("Books");
        modelBuilder.Entity<Film>().ToTable("Movies");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BookStoreDatabase; Trusted_Connection = True; "); 
    }
}




