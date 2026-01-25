using FoundersDesk.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FoundersDesk.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<ResourceAcknowledgement> ResourceAcknowledgements { get; set; }
        public DbSet<DigitalSignature> DigitalSignatures { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<TrainingResource> TrainingResources { get; set; }






        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========================== USER TABLE ==========================
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Role).HasConversion<int>();
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).ValueGeneratedOnAdd();
                entity.Property(e => e.IsProfileCompleted).HasDefaultValue(false);

            });

            // ========================== DEFAULT LOGIN USERS ==========================
            


            // ========================== SESSION ==========================
            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => e.SessionId);
                entity.HasIndex(e => e.SessionToken).IsUnique();
            });

            // ========================== VIDEO ==========================
            modelBuilder.Entity<Video>(entity =>
            {
                entity.HasKey(e => e.VideoId);
            });

            modelBuilder.Entity<Course>().HasKey(c => c.CourseId);
            modelBuilder.Entity<Module>().HasKey(m => m.ModuleId);


        }
    }
}
