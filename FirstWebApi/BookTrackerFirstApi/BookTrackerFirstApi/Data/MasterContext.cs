
using Microsoft.EntityFrameworkCore;
using BookTrackerFirstApi.Models;
namespace BookTrackerFirstApi.Data;

public class MasterContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = FirstWebApi; Trusted_Connection = True;");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().ToTable("Books").HasData(
        new Book { Id = 1, Name = "Na Drini ćuprija", Author = "Ivo Andrić", Description = "Istorijski roman o mostu.", IsRead = true },
        new Book { Id = 2, Name = "Tvrđava", Author = "Meša Selimović", Description = "Psihološka drama o pojedincu i sistemu.", IsRead = false },
        new Book { Id = 3, Name = "Clean Code", Author = "Robert C. Martin", Description = "Biblija za developere.", IsRead = true },
        new Book { Id = 4, Name = "The Pragmatic Programmer", Author = "Andrew Hunt", Description = "Savjeti za moderan razvoj softvera.", IsRead = false }
    );
    }




    }

