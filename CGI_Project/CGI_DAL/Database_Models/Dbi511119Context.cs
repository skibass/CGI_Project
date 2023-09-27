using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CGI_Models;

namespace CGI_DAL.Database_Models;

public partial class Dbi511119Context : DbContext
{
    public Dbi511119Context()
    {
    }

    public Dbi511119Context(DbContextOptions<Dbi511119Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Period> Periods { get; set; }

    public virtual DbSet<Poll> Polls { get; set; }

    public virtual DbSet<PollSuggestion> PollSuggestions { get; set; }

    public virtual DbSet<Right> Rights { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleRight> RoleRights { get; set; }

    public virtual DbSet<Suggestion> Suggestions { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=studmysql01.fhict.local;UID=dbi511119;Pwd=TeamKever;Database=dbi511119;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("company");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("employee");

            entity.HasIndex(e => e.CompanyId, "FK_Employee-Company_idx");

            entity.HasIndex(e => e.RoleId, "FK_Employee-Role_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CompanyId)
                .HasColumnType("int(11)")
                .HasColumnName("companyId");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");
            entity.Property(e => e.ProfileImage)
                .HasMaxLength(45)
                .HasColumnName("profileImage");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("roleId");

            entity.HasOne(d => d.Company).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_Employee-Company");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Employee-Role");
        });

        modelBuilder.Entity<Period>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("period");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.BeginDate)
                .HasColumnType("datetime")
                .HasColumnName("beginDate");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
        });

        modelBuilder.Entity<Poll>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("poll");

            entity.HasIndex(e => e.ManagerId, "FK_Poll-Employee_idx");

            entity.HasIndex(e => e.PeriodId, "FK_Poll-Period_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("endTime");
            entity.Property(e => e.ManagerId)
                .HasColumnType("int(11)")
                .HasColumnName("managerId");
            entity.Property(e => e.PeriodId)
                .HasColumnType("int(11)")
                .HasColumnName("periodId");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("startTime");

            entity.HasOne(d => d.Manager).WithMany(p => p.Polls)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK_Poll-Employee");

            entity.HasOne(d => d.Period).WithMany(p => p.Polls)
                .HasForeignKey(d => d.PeriodId)
                .HasConstraintName("FK_Poll-Period");
        });

        modelBuilder.Entity<PollSuggestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("poll_suggestion");

            entity.HasIndex(e => e.PollId, "FK_PollSuggestion-Poll_idx");

            entity.HasIndex(e => e.SuggestionId, "FK_PollSuggestion-Suggestion_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.PollId)
                .HasColumnType("int(11)")
                .HasColumnName("pollId");
            entity.Property(e => e.SuggestionId)
                .HasColumnType("int(11)")
                .HasColumnName("suggestionId");

            entity.HasOne(d => d.Poll).WithMany(p => p.PollSuggestions)
                .HasForeignKey(d => d.PollId)
                .HasConstraintName("FK_PollSuggestion-Poll");

            entity.HasOne(d => d.Suggestion).WithMany(p => p.PollSuggestions)
                .HasForeignKey(d => d.SuggestionId)
                .HasConstraintName("FK_PollSuggestion-Suggestion");
        });

        modelBuilder.Entity<Right>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rights");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(45)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(45)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<RoleRight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role_rights");

            entity.HasIndex(e => e.RoleId, "FK_Role_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.RightId)
                .HasColumnType("int(11)")
                .HasColumnName("rightId");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("roleId");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleRights)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Right");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.RoleRights)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Role");
        });

        modelBuilder.Entity<Suggestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("suggestion");

            entity.HasIndex(e => e.EmployeeId, "FK_Suggestion-Employee_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(45)
                .HasColumnName("description");
            entity.Property(e => e.EmployeeId)
                .HasColumnType("int(11)")
                .HasColumnName("employeeId");
            entity.Property(e => e.Exception)
                .HasMaxLength(45)
                .HasColumnName("exception");
            entity.Property(e => e.Location)
                .HasMaxLength(45)
                .HasColumnName("location");

            entity.HasOne(d => d.Employee).WithMany(p => p.Suggestions)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Suggestion-Employee");
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("vote");

            entity.HasIndex(e => e.EmployeeId, "FK_Vote-Employee_idx");

            entity.HasIndex(e => e.SuggestionId, "FK_Vote-Suggestion_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.EmployeeId)
                .HasColumnType("int(11)")
                .HasColumnName("employeeId");
            entity.Property(e => e.SuggestionId)
                .HasColumnType("int(11)")
                .HasColumnName("suggestionId");

            entity.HasOne(d => d.Employee).WithMany(p => p.Votes)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Vote-Employee");

            entity.HasOne(d => d.Suggestion).WithMany(p => p.Votes)
                .HasForeignKey(d => d.SuggestionId)
                .HasConstraintName("FK_Vote-Suggestion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
