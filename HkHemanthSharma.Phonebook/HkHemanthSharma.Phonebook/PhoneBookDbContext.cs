using HkHemanthSharma.Phonebook.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace HkHemanthSharma.Phonebook
{
    public class PhoneBookDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Contact>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Contact>()
                .Property(o => o.ContactName)
                .HasMaxLength(15)
                .IsRequired();
            modelBuilder.Entity<Contact>()
                .Property(o => o.Email)
                .HasAnnotation("RegularExpression",
            @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            modelBuilder.Entity<Contact>()
                .Property(o => o.Email)
                .HasConversion(o => Validations.ValidateEmail(o),
                              o => o)
                .IsRequired();
            modelBuilder.Entity<Contact>()
                .Property(o => o.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength()
                .IsRequired()
                .HasAnnotation(
        "RegularExpression", @"^\d{10}$");
            modelBuilder.Entity<Contact>()
                .HasData(
                new Contact
                {
                    Id = 1,
                    ContactName = "Hemanth",
                    Email = "HkHemanthSharma@gmail.com",
                    PhoneNumber = "7013558608",
                    Address = "3-14,Pakala,India",
                    CategoryId = 1
                },
                new Contact
                {
                    Id = 2,
                    ContactName = "ViratKohli",
                    Email = "ViratKohli@gmail.com",
                    PhoneNumber = "0123456789",
                    Address = "building no:3,BeachRoad,Juhu,mumbai,India",
                    CategoryId = 2
                },
                new Contact
                {
                    Id = 3,
                    ContactName = "Lionel Messi",
                    Email = "LionelMessi@Gov.in",
                    PhoneNumber = "9874561230",
                    Address = "Estate:573,PocoGarden Suburbs,Buenis Aires,Argentaina",
                    CategoryId = 3
                }
                );

            modelBuilder.Entity<Category>()
                .HasMany(o => o.Contacts)
                .WithOne(o => o.Category)
                .HasForeignKey(o => o.CategoryId);
            modelBuilder.Entity<Category>()
                .HasData(
                new Category
                {
                    Id = 1,
                    CategoryName = "Family"
                },
                new Category
                {
                    Id = 2,
                    CategoryName = "Friends"
                },
                new Category
                {
                    Id = 3,
                    CategoryName = "Work"
                },
                new Category
                {
                    Id = 4,
                    CategoryName = "Others"
                });
            modelBuilder.Entity<Category>().HasKey(o => o.Id);
        }
    }
}

