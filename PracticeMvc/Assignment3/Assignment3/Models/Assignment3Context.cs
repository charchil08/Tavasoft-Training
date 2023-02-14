using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Assignment3.Models
{
    public partial class Assignment3Context : DbContext
    {
        public Assignment3Context()
        {
        }

        public Assignment3Context(DbContextOptions<Assignment3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<VwMaxSalaryByDepartment> VwMaxSalaryByDepartments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepId)
                    .HasName("PK__Departme__BB4BD8F8875A5631");

                entity.ToTable("Department");

                entity.Property(e => e.DepId)
                    .ValueGeneratedNever()
                    .HasColumnName("dep_id");

                entity.Property(e => e.DepLocation)
                    .HasMaxLength(30)
                    .HasColumnName("dep_location");

                entity.Property(e => e.DepName)
                    .HasMaxLength(30)
                    .HasColumnName("dep_name");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__Employee__1299A86137929F5D");

                entity.Property(e => e.EmpId)
                    .ValueGeneratedNever()
                    .HasColumnName("emp_id");

                entity.Property(e => e.Commission)
                    .HasColumnType("numeric(6, 2)")
                    .HasColumnName("commission");

                entity.Property(e => e.DepId).HasColumnName("dep_id");

                entity.Property(e => e.EmpName)
                    .HasMaxLength(50)
                    .HasColumnName("emp_name");

                entity.Property(e => e.HireDate)
                    .HasColumnType("date")
                    .HasColumnName("hire_date");

                entity.Property(e => e.JobName)
                    .HasMaxLength(50)
                    .HasColumnName("job_name");

                entity.Property(e => e.ManagerId).HasColumnName("manager_id");

                entity.Property(e => e.Salary)
                    .HasColumnType("numeric(7, 2)")
                    .HasColumnName("salary");

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepId)
                    .HasConstraintName("FK__Employees__dep_i__3A81B327");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__Employees__manag__398D8EEE");
            });

            modelBuilder.Entity<VwMaxSalaryByDepartment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwMaxSalaryByDepartment");

                entity.Property(e => e.DepId).HasColumnName("dep_id");

                entity.Property(e => e.Salary)
                    .HasColumnType("numeric(7, 2)")
                    .HasColumnName("salary");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
