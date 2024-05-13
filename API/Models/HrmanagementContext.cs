using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class HrmanagementContext : DbContext
{
    public HrmanagementContext()
    {
    }

    public HrmanagementContext(DbContextOptions<HrmanagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Claim> Claims { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<User> Users { get; set; }

    private string? GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DBDefault"];
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.ToTable("Application");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FileAttach).HasColumnName("fileAttach");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.Applications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Application_User");
        });

        modelBuilder.Entity<Claim>(entity =>
        {
            entity.ToTable("Claim");

            entity.Property(e => e.Id)
                .HasMaxLength(250)
                .HasColumnName("ID");
            entity.Property(e => e.Content).HasColumnName("content");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.ToTable("Position");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.WorkingLocation)
                .HasMaxLength(50)
                .HasColumnName("workingLocation");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .HasMaxLength(250)
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.ToTable("Salary");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Absent).HasColumnName("absent");
            entity.Property(e => e.FixedAmount).HasColumnName("fixedAmount");
            entity.Property(e => e.OnFurlough).HasColumnName("onFurlough");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("phone");
            entity.Property(e => e.PositionId).HasColumnName("positionID");
            entity.Property(e => e.SalaryId).HasColumnName("salaryID");

            entity.HasOne(d => d.Position).WithMany(p => p.Users)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_User_Position");

            entity.HasOne(d => d.Salary).WithMany(p => p.Users)
                .HasForeignKey(d => d.SalaryId)
                .HasConstraintName("FK_User_Salary");

            entity.HasMany(d => d.Claims).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserClaim",
                    r => r.HasOne<Claim>().WithMany()
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserClaim__claim__76969D2E"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserClaim__userI__75A278F5"),
                    j =>
                    {
                        j.HasKey("UserId", "ClaimId").HasName("PK__UserClai__EB81C344C9F655A7");
                        j.ToTable("UserClaim");
                        j.IndexerProperty<Guid>("UserId").HasColumnName("userID");
                        j.IndexerProperty<string>("ClaimId")
                            .HasMaxLength(250)
                            .HasColumnName("claimID");
                    });

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRole__roleID__72C60C4A"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRole__userID__71D1E811"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__UserRole__774398BF097C0872");
                        j.ToTable("UserRole");
                        j.IndexerProperty<Guid>("UserId").HasColumnName("userID");
                        j.IndexerProperty<string>("RoleId")
                            .HasMaxLength(250)
                            .HasColumnName("roleID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
