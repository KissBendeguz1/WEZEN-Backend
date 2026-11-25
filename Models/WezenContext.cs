using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Wezen_Backend.Models;

public partial class WezenContext : DbContext
{
    public WezenContext()
    {
    }

    public WezenContext(DbContextOptions<WezenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDataGoal> UserDataGoals { get; set; }

    public virtual DbSet<UserDatum> UserData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role");

            entity.HasIndex(e => e.RoleName, "uq_role_name").IsUnique();

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_Name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.RoleId, "Role_Id");

            entity.HasIndex(e => e.Email, "uq_user_email").IsUnique();

            entity.HasIndex(e => e.Username, "uq_user_username").IsUnique();

            entity.Property(e => e.Id).HasMaxLength(36);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            entity.Property(e => e.Salt).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_ibfk_1");
        });

        modelBuilder.Entity<UserDataGoal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_data_goal");

            entity.Property(e => e.Id).HasMaxLength(36);
            entity.Property(e => e.Calcium).HasColumnName("calcium");
            entity.Property(e => e.Calories).HasColumnName("calories");
            entity.Property(e => e.Carbs).HasColumnName("carbs");
            entity.Property(e => e.Colesterol).HasColumnName("colesterol");
            entity.Property(e => e.Fat).HasColumnName("fat");
            entity.Property(e => e.Fiber).HasColumnName("fiber");
            entity.Property(e => e.Iron).HasColumnName("iron");
            entity.Property(e => e.Potassium).HasColumnName("potassium");
            entity.Property(e => e.Protein).HasColumnName("protein");
            entity.Property(e => e.Sodium).HasColumnName("sodium");
            entity.Property(e => e.Sugar).HasColumnName("sugar");
            entity.Property(e => e.Water).HasColumnName("water");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.UserDataGoal)
                .HasForeignKey<UserDataGoal>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_data_goal_ibfk_1");
        });

        modelBuilder.Entity<UserDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_data");

            entity.Property(e => e.Id).HasMaxLength(36);
            entity.Property(e => e.DailyCalcium).HasColumnName("daily_calcium");
            entity.Property(e => e.DailyCalories).HasColumnName("daily_calories");
            entity.Property(e => e.DailyCarbs).HasColumnName("daily_carbs");
            entity.Property(e => e.DailyColesterol).HasColumnName("daily_colesterol");
            entity.Property(e => e.DailyFat).HasColumnName("daily_fat");
            entity.Property(e => e.DailyFiber).HasColumnName("daily_fiber");
            entity.Property(e => e.DailyIron).HasColumnName("daily_iron");
            entity.Property(e => e.DailyPotassium).HasColumnName("daily_potassium");
            entity.Property(e => e.DailyProtein).HasColumnName("daily_protein");
            entity.Property(e => e.DailySodium).HasColumnName("daily_sodium");
            entity.Property(e => e.DailySugar).HasColumnName("daily_sugar");
            entity.Property(e => e.DailyWater).HasColumnName("daily_water");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.UserDatum)
                .HasForeignKey<UserDatum>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_data_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
