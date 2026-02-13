using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;

namespace Data.Context
{
    public class MasterContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = Exam2; Trusted_Connection = True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>().ToTable("Workers").HasData(
                new Worker { Id = 1, Name = "Dženan Kazazović", Position = "Backend Developer", ExperienceInYears = 2.5 },
                new Worker { Id = 2, Name = "Amar Delić", Position = "Frontend Developer", ExperienceInYears = 3.0 },
                new Worker { Id = 3, Name = "Lejla Hodžić", Position = "DevOps Engineer", ExperienceInYears = 1.2 },
                new Worker { Id = 4, Name = "Sara Ibrahimović", Position = "Junior QA", ExperienceInYears = null }
                );

            modelBuilder.Entity<Client>().ToTable("Clients").HasData(
                new { Id = 5, Name = "Atlantbh", CompanyName = "Atlantbh d.o.o.", Email = "info@atlantbh.com" },
                new { Id = 6, Name = "Microsoft", CompanyName = "Microsoft Corporation", Email = "support@microsoft.com" },
                new { Id = 7, Name = "Google", CompanyName = "Google LLC", Email = (string?)null },
                new { Id = 8, Name = "Mistral", CompanyName = "Mistral Technologies", Email = "contact@mistral.ba" }
                );
        }

    }
}
