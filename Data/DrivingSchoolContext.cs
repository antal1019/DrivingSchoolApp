using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolApp.Models;

namespace DrivingSchoolApp.Data
{
    public class DrivingSchoolContext : IdentityDbContext<ApplicationUser>
    {
        public DrivingSchoolContext(DbContextOptions<DrivingSchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Student)
                .WithMany(s => s.Lessons)
                .HasForeignKey(l => l.StudentID);

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Instructor)
                .WithMany(i => i.Lessons)
                .HasForeignKey(l => l.InstructorID);

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Vehicle)
                .WithMany(v => v.Lessons)
                .HasForeignKey(l => l.VehicleID);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(au => au.Student)
                .WithOne()
                .HasForeignKey<ApplicationUser>(au => au.StudentId)
                .IsRequired(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(au => au.Instructor)
                .WithOne()
                .HasForeignKey<ApplicationUser>(au => au.InstructorId)
                .IsRequired(false);
        }
    }
}