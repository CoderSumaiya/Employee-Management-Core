using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcCore_employeeProject.Models;

namespace MvcCore_employeeProject.Data
{
    public class AppDbContext : IdentityDbContext
    {
        // Stick to the constructor that accepts options for Dependency Injection
        public AppDbContext(DbContextOptions<AppDbContext> op) : base(op)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<AcademicDetail> AcademicDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Always call the base first for Identity tables
            base.OnModelCreating(modelBuilder);

        
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");
            });


            modelBuilder.Entity<AcademicDetail>(entity =>
            {
                entity.Property(a => a.CGPA).HasColumnType("decimal(18,2)");
            });
            modelBuilder.Seed();
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "IT" },
                new Department { DepartmentId = 2, DepartmentName = "HR" },
                new Department { DepartmentId = 3, DepartmentName = "Account" }
            );
        }
    }
}