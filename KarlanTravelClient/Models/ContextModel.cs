using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace KarlanTravelClient.Models
{
    public partial class ContextModel : DbContext
    {
        public ContextModel()
            : base("name=ContextModel")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AdminRole> AdminRoles { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<FacilityType> FacilityTypes { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<TourDetail> TourDetails { get; set; }
        public virtual DbSet<TouristSpot> TouristSpots { get; set; }
        public virtual DbSet<TransactionRecord> TransactionRecords { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminRole>()
                .HasMany(e => e.Admins)
                .WithRequired(e => e.AdminRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bank>()
                .HasMany(e => e.BankAccounts)
                .WithRequired(e => e.Bank)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.SubCategories)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Tours)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.CategoryId1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Tours1)
                .WithOptional(e => e.Category1)
                .HasForeignKey(e => e.CategoryId2);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Customers)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Facilities)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.TouristSpots)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Banks)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Cities)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.AmountToPay)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.TransactionRecords)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Facility>()
                .HasMany(e => e.TourDetails)
                .WithRequired(e => e.Facility)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FacilityType>()
                .HasMany(e => e.Facilities)
                .WithRequired(e => e.FacilityType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubCategory>()
                .HasMany(e => e.TouristSpots)
                .WithRequired(e => e.SubCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tour>()
                .Property(e => e.TourPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Tour>()
                .HasMany(e => e.TourDetails)
                .WithRequired(e => e.Tour)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tour>()
                .HasMany(e => e.TransactionRecords)
                .WithRequired(e => e.Tour)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TouristSpot>()
                .HasMany(e => e.TourDetails)
                .WithRequired(e => e.TouristSpot)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TransactionRecord>()
                .Property(e => e.TransactionFee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TransactionType>()
                .HasMany(e => e.TransactionRecords)
                .WithRequired(e => e.TransactionType)
                .WillCascadeOnDelete(false);
        }
    }
}
