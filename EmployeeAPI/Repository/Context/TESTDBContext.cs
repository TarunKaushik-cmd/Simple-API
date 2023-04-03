using System;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EmployeeAPI.Repository.Context
{
    public partial class TESTDBContext : IdentityDbContext<ApplicationUser>
    {
        public TESTDBContext()
        {
        }

        public TESTDBContext(DbContextOptions<TESTDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<ExceptionRecords> ExceptionRecordss { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DBConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            //modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            //modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
            //modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptId)
                    .HasName("PK__DEPARTME__0148818E1FA462CB");

                entity.ToTable("DEPARTMENT");

                entity.Property(e => e.DeptId).HasColumnName("DeptID");

                entity.Property(e => e.DeptName).HasMaxLength(20);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__EMPLOYEE__AF2DBA790EBBD37D");

                entity.ToTable("EMPLOYEE");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.Address).HasMaxLength(20);

                entity.Property(e => e.DeptId).HasColumnName("DeptID");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Qualification).HasMaxLength(10);

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DeptId)
                    .HasConstraintName("FK__EMPLOYEE__DeptID__2D27B809");
            });
            modelBuilder.Entity<ExceptionRecords>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Exceptio__3214EC27F46C68DF");

                entity.ToTable("ExceptionRecords");
                entity.Property(e => e.ExceptionType).HasColumnName("ExceptionType");

                entity.Property(e => e.InnerException).HasColumnName("InnerException");

                entity.Property(e => e.Message).HasColumnName("Message");

                entity.Property(e => e.CreatedDate).HasColumnName("CreatedDate");
            });
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
