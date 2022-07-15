﻿// <auto-generated />
using System;
using GameShop.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameShop.Data.Migrations
{
    [DbContext(typeof(GameShopDbContext))]
    partial class GameShopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameShop.Data.Entities.BaseEntity", b =>
                {
                    b.Property<int>("BaseEntityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BaseEntityID");

                    b.ToTable("BaseEntity");
                });

            modelBuilder.Entity("GameShop.Data.Entities.Game", b =>
                {
                    b.Property<int>("GameID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BaseEntityID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Discount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gameplay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("GameID");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            GameID = 1,
                            BaseEntityID = 0,
                            CreatedDate = new DateTime(2022, 7, 14, 15, 44, 22, 460, DateTimeKind.Local).AddTicks(3781),
                            Description = "The best game in the world",
                            Discount = 0,
                            GameName = "Grand Theft Auto V",
                            Gameplay = "Destroy the city",
                            Price = 250000m,
                            Status = 1,
                            UpdatedDate = new DateTime(2022, 7, 14, 15, 44, 22, 461, DateTimeKind.Local).AddTicks(2153)
                        });
                });

            modelBuilder.Entity("GameShop.Data.Entities.GameinGenre", b =>
                {
                    b.Property<int>("GenreID")
                        .HasColumnType("int");

                    b.Property<int>("GameID")
                        .HasColumnType("int");

                    b.HasKey("GenreID", "GameID");

                    b.HasIndex("GameID");

                    b.ToTable("GameinGenre");

                    b.HasData(
                        new
                        {
                            GenreID = 1,
                            GameID = 1
                        },
                        new
                        {
                            GenreID = 2,
                            GameID = 1
                        });
                });

            modelBuilder.Entity("GameShop.Data.Entities.Genre", b =>
                {
                    b.Property<int>("GenreID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GenreName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreID");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            GenreID = 1,
                            GenreName = "Action"
                        },
                        new
                        {
                            GenreID = 2,
                            GenreName = "Open-World"
                        },
                        new
                        {
                            GenreID = 3,
                            GenreName = "Multiplayer"
                        });
                });

            modelBuilder.Entity("GameShop.Data.Entities.SystemRequirementMin", b =>
                {
                    b.Property<int>("SRMID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameID")
                        .HasColumnType("int");

                    b.Property<string>("Graphics")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Memory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Processor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Storage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SRMID");

                    b.HasIndex("GameID")
                        .IsUnique();

                    b.ToTable("SystemRequirementMin");
                });

            modelBuilder.Entity("GameShop.Data.Entities.SystemRequirementRecommended", b =>
                {
                    b.Property<int>("SRRID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameID")
                        .HasColumnType("int");

                    b.Property<string>("Graphics")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Memory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Processor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Storage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SRRID");

                    b.HasIndex("GameID")
                        .IsUnique();

                    b.ToTable("SystemRequirementRecommended");
                });

            modelBuilder.Entity("GameShop.Data.Entities.GameinGenre", b =>
                {
                    b.HasOne("GameShop.Data.Entities.Game", "Game")
                        .WithMany("GameInGenres")
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameShop.Data.Entities.Genre", "Genre")
                        .WithMany("GameInGenres")
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameShop.Data.Entities.SystemRequirementMin", b =>
                {
                    b.HasOne("GameShop.Data.Entities.Game", "Game")
                        .WithOne("SystemRequirementMin")
                        .HasForeignKey("GameShop.Data.Entities.SystemRequirementMin", "GameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameShop.Data.Entities.SystemRequirementRecommended", b =>
                {
                    b.HasOne("GameShop.Data.Entities.Game", "Game")
                        .WithOne("SystemRequirementRecommended")
                        .HasForeignKey("GameShop.Data.Entities.SystemRequirementRecommended", "GameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
