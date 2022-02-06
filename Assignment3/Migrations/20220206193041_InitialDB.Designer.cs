﻿// <auto-generated />
using System;
using Assignment3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Assignment3.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    [Migration("20220206193041_InitialDB")]
    partial class InitialDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Assignment3.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alias")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Photo")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Character");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Alias = "Rocky",
                            Gender = "Male",
                            Name = "Robert Balboa",
                            Photo = "https://upload.wikimedia.org/wikipedia/en/5/53/Rocky_balboa.jpeg"
                        },
                        new
                        {
                            Id = 2,
                            Alias = "Adrian",
                            Gender = "Female",
                            Name = "Adriana Pennino",
                            Photo = "https://upload.wikimedia.org/wikipedia/commons/c/cd/Talia_Shire_1976_edit.JPG"
                        },
                        new
                        {
                            Id = 3,
                            Alias = "Mickey",
                            Gender = "Male",
                            Name = "Michael Goldmill",
                            Photo = "https://upload.wikimedia.org/wikipedia/en/7/7b/Mickey_Goldmill.jpg"
                        });
                });

            modelBuilder.Entity("Assignment3.Models.Franchise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Franchise");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Movie about you get knocked down and you get back up again",
                            Name = "Rocky"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Movie about some shorties carrying some jewelry",
                            Name = "Lord of the Rings"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Movie 'bout some wizard bloke",
                            Name = "Harry Potter"
                        });
                });

            modelBuilder.Entity("Assignment3.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("FranchiseId")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Picture")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("ReleaseYear")
                        .HasMaxLength(4)
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Trailer")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("FranchiseId");

                    b.ToTable("Movie");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Director = "John G. Avildsen",
                            FranchiseId = 1,
                            Genre = "Sports Drama",
                            ReleaseYear = 1976,
                            Title = "Rocky"
                        },
                        new
                        {
                            Id = 2,
                            Director = "Sylvester Stallone",
                            FranchiseId = 1,
                            Genre = "Sports Drama",
                            ReleaseYear = 1979,
                            Title = "Rocky II"
                        },
                        new
                        {
                            Id = 3,
                            Director = "Sylvester Stallone",
                            FranchiseId = 1,
                            Genre = "Sports Drama",
                            ReleaseYear = 1982,
                            Title = "Rocky III"
                        });
                });

            modelBuilder.Entity("MovieCharacters", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("CharacterId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieCharacters");

                    b.HasData(
                        new
                        {
                            CharacterId = 1,
                            MovieId = 1
                        },
                        new
                        {
                            CharacterId = 2,
                            MovieId = 1
                        },
                        new
                        {
                            CharacterId = 3,
                            MovieId = 2
                        });
                });

            modelBuilder.Entity("Assignment3.Models.Movie", b =>
                {
                    b.HasOne("Assignment3.Models.Franchise", "Franchise")
                        .WithMany("Movies")
                        .HasForeignKey("FranchiseId");

                    b.Navigation("Franchise");
                });

            modelBuilder.Entity("MovieCharacters", b =>
                {
                    b.HasOne("Assignment3.Models.Character", null)
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignment3.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Assignment3.Models.Franchise", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
