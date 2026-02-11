

using Domain;
using Microsoft.EntityFrameworkCore;
public class MasterContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<Project> Projects { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = ProjectTeamsManagment; Trusted_Connection = True;");
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().ToTable("TeamMembers");
        modelBuilder.Entity<Project>().ToTable("Projects");
    }





}




