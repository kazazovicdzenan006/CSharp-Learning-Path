

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyFirstEfCoreDatabase;Trusted_Connection=True;");
    }
}





