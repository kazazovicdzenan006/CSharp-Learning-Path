

using Microsoft.EntityFrameworkCore;

public class CityContext : DbContext {
    public DbSet<CityNode> CityNodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CityNode>().ToTable("CityNodes");
        modelBuilder.Entity<ParkingLot>().ToTable("ParkingLots");
        modelBuilder.Entity<CrossRoad>().ToTable("CrossRoads");

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = SmartCityDatabase; Trusted_Connection = True;");
    }
    

}




