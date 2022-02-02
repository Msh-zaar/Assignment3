using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assignment3.Models
{
    public class MovieDbContext : DbContext
    {
        // Tables
        public DbSet<Character> Characters { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public MovieDbContext([NotNullAttribute] DbContextOptions options) : base(options) 
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // hjemme DESKTOP-8LK9FE7\\SQLEXPRESS
            // skole ND-5CG8473X7D\\SQLEXPRESS
            base.OnConfiguring(optionsBuilder); 
            optionsBuilder.UseSqlServer("Data Source=ND-5CG8473X7D\\SQLEXPRESS;Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<Character>()
                .HasData(new Character { Id = 1, Name = "Robert Balboa", Alias = "Rocky", Gender = "Male", Photo = "https://en.wikipedia.org/wiki/File:Rocky_balboa.jpeg" });
            modelBuilder.Entity<Character>()
                .HasData(new Character { Id = 2, Name = "Adriana Pennino", Alias = "Adrian", Gender = "Female", Photo = "https://en.wikipedia.org/wiki/File:Talia_Shire_1976_edit.JPG" });
            modelBuilder.Entity<Character>()
                .HasData(new Character { Id = 3, Name = "Michael Goldmill", Alias = "Mickey", Gender = "Male", Photo = "https://en.wikipedia.org/wiki/File:Mickey_Goldmill.jpg" });

            modelBuilder.Entity<Movie>()
                .HasData(new Movie { Id = 1, Title = "Rocky", Genre = "Sports Drama", Director = "John G. Avildsen", ReleaseYear = 1976, Picture = "", Trailer = "", FranchiseId = 1});
            modelBuilder.Entity<Movie>()
               .HasData(new Movie { Id = 2, Title = "Rocky II", Genre = "Sports Drama", Director = "Sylvester Stallone", ReleaseYear = 1979, Picture = "", Trailer = "", FranchiseId = 1 });
            modelBuilder.Entity<Movie>()
               .HasData(new Movie { Id = 3, Title = "Rocky III", Genre = "Sports Drama", Director = "Sylvester Stallone", ReleaseYear = 1982, Picture = "", Trailer = "", FranchiseId = 1 });

            modelBuilder.Entity<Franchise>()
                .HasData(new Franchise { Id = 1, Name = "Rocky", Description = "" });
            modelBuilder.Entity<Franchise>()
                .HasData(new Franchise { Id = 2, Name = "Lord of the Rings", Description = "" });
            modelBuilder.Entity<Franchise>()
                .HasData(new Franchise { Id = 3, Name = "HArry Potter", Description = "" });

            // Seed m2m
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Characters)
                .WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                "MovieCharacters",
                r => r.HasOne<Character>().WithMany().HasForeignKey("CharacterId"),
                l => l.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                je =>
                {
                    je.HasKey("CharacterId", "MovieId");
                    je.HasData(
                        new { CharacterId = 1, MovieId = 1 },
                        new { CharacterId = 2, MovieId = 1 }
                        );
                });
        }
    }
}
