using Microsoft.EntityFrameworkCore;
using DeviceManagementSystem.Models;

namespace DeviceManagementSystem.Data
{
    /// <summary>
    /// Database context for the Device Management System
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DeviceCategory> DeviceCategories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure DeviceCategory entity
            modelBuilder.Entity<DeviceCategory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                
                // Seed data for device categories
                entity.HasData(
                    new DeviceCategory { Id = 1, Name = "Computer", Description = "Desktop and laptop computers" },
                    new DeviceCategory { Id = 2, Name = "Printer", Description = "Printers and scanners" },
                    new DeviceCategory { Id = 3, Name = "Phone", Description = "Mobile phones and landlines" },
                    new DeviceCategory { Id = 4, Name = "Network Device", Description = "Routers, switches, and network equipment" },
                    new DeviceCategory { Id = 5, Name = "Other", Description = "Miscellaneous devices" }
                );
            });

            // Configure Device entity
            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.DeviceCode).IsRequired().HasMaxLength(50);
                entity.Property(e => e.DateOfEntry).IsRequired();
                
                // Configure foreign key relationship
                entity.HasOne(d => d.DeviceCategory)
                    .WithMany(c => c.Devices)
                    .HasForeignKey(d => d.DeviceCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
                
                // Ensure email is unique
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
} 