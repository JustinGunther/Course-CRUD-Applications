using System;
using CRUDApps.DataAccess.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CRUDApps.DataAccess.EF.Context
{
    public partial class SQLFundamentalsContext : DbContext
    {        

        public SQLFundamentalsContext()
        {
            
        }

        public SQLFundamentalsContext(DbContextOptions<SQLFundamentalsContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<Banking> Banking { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<MyRecipes> MyRecipes { get; set; }
        public virtual DbSet<Pets> Pets { get; set; }
        public virtual DbSet<RaceResults> RaceResults { get; set; }
        public virtual DbSet<TeamRecords> TeamRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banking>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AccountBalance).HasColumnType("decimal(14, 2)");

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(2550);
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.HasKey(e => e.BookId);

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.BookTitle)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Contacts>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Documents>(entity =>
            {
                entity.HasKey(e => e.DocumentId);

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.DocumentName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastUpdateDate).HasColumnType("date");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Cost).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Movies>(entity =>
            {
                entity.HasKey(e => e.MovieId);

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.Director)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LeadActor)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MovieTitle)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<MyRecipes>(entity =>
            {
                entity.HasKey(e => e.RecipeId);

                entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

                entity.Property(e => e.MainIngredient)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MealType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RecipeName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Pets>(entity =>
            {
                entity.HasKey(e => e.PetId);

                entity.Property(e => e.PetId).HasColumnName("PetID");

                entity.Property(e => e.Food)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PetName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Species)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Veterinarian)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<RaceResults>(entity =>
            {
                entity.HasKey(e => e.RunnerId);

                entity.Property(e => e.RunnerId).HasColumnName("RunnerID");

                entity.Property(e => e.BibNumber)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(2);
            });

            modelBuilder.Entity<TeamRecords>(entity =>
            {
                entity.HasKey(e => e.TeamId);

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e._2017season)
                    .IsRequired()
                    .HasColumnName("2017Season")
                    .HasMaxLength(50);

                entity.Property(e => e._2018season)
                    .IsRequired()
                    .HasColumnName("2018Season")
                    .HasMaxLength(50);

                entity.Property(e => e._2019season)
                    .IsRequired()
                    .HasColumnName("2019Season")
                    .HasMaxLength(50);

                entity.Property(e => e._2020season)
                    .IsRequired()
                    .HasColumnName("2020Season")
                    .HasMaxLength(50);

                entity.Property(e => e._2021season)
                    .IsRequired()
                    .HasColumnName("2021Season")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
