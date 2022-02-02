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

        public MovieDbContext(DbContextOptions options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<Character>()
                .HasData(new Character { Id = 1, Name = "Robert Balboa", Alias = "Rocky", Gender = "Male", Photo = "https://upload.wikimedia.org/wikipedia/en/5/53/Rocky_balboa.jpeg" });
            modelBuilder.Entity<Character>()
                .HasData(new Character { Id = 2, Name = "Adriana Pennino", Alias = "Adrian", Gender = "Female", Photo = "https://upload.wikimedia.org/wikipedia/commons/c/cd/Talia_Shire_1976_edit.JPG" });
            modelBuilder.Entity<Character>()
                .HasData(new Character { Id = 3, Name = "Michael Goldmill", Alias = "Mickey", Gender = "Male", Photo = "https://upload.wikimedia.org/wikipedia/en/7/7b/Mickey_Goldmill.jpg" });

            modelBuilder.Entity<Movie>()
                .HasData(new Movie { Id = 1, Title = "Rocky", Genre = "Sports Drama", Director = "John G. Avildsen", ReleaseYear = 1976, FranchiseId = 1});
            modelBuilder.Entity<Movie>()
               .HasData(new Movie { Id = 2, Title = "Rocky II", Genre = "Sports Drama", Director = "Sylvester Stallone", ReleaseYear = 1979, FranchiseId = 1 });
            modelBuilder.Entity<Movie>()
               .HasData(new Movie { Id = 3, Title = "Rocky III", Genre = "Sports Drama", Director = "Sylvester Stallone", ReleaseYear = 1982, FranchiseId = 1 });

            modelBuilder.Entity<Franchise>()
                .HasData(new Franchise { Id = 1, Name = "Rocky", Description = "Movie about you get knocked down and you get back up again" });
            modelBuilder.Entity<Franchise>()
                .HasData(new Franchise { Id = 2, Name = "Lord of the Rings", Description = "Movie about some shorties carrying some jewelry" });
            modelBuilder.Entity<Franchise>()
                .HasData(new Franchise { Id = 3, Name = "Harry Potter", Description = "Movie 'bout some wizard bloke" });

            // Seed m2m
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Characters)
                .WithMany(c => c.Movies)
                .UsingEntity<Dictionary<string, object>>(
                "MovieCharacters",
                right => right.HasOne<Character>().WithMany().HasForeignKey("CharacterId"),
                left => left.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                join =>
                {
                    join.HasKey("CharacterId", "MovieId");
                    join.HasData(
                        new { CharacterId = 1, MovieId = 1 },
                        new { CharacterId = 2, MovieId = 1 },
                        new { CharacterId = 3, MovieId = 2 }
                        );
                });
        }
    }
}
