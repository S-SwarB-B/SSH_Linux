using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SSHProject.DB;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ErrorImportance> ErrorImportances { get; set; }

    public virtual DbSet<Problem> Problems { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("pg_catalog", "adminpack")
            .HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<ErrorImportance>(entity =>
        {
            entity.HasKey(e => e.IdErrorImportance).HasName("error_importances_pkey");

            entity.ToTable("error_importances");

            entity.Property(e => e.IdErrorImportance)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id_error_importance");
            entity.Property(e => e.NameErrorImportances)
                .HasColumnType("character varying")
                .HasColumnName("name_error_importances");
        });

        modelBuilder.Entity<Problem>(entity =>
        {
            entity.HasKey(e => e.IdProblem).HasName("problems_pkey");

            entity.ToTable("problems");

            entity.Property(e => e.IdProblem)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id_problem");
            entity.Property(e => e.DateProblemSolution)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_problem_solution");
            entity.Property(e => e.DateTimeProblem)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_time_problem");
            entity.Property(e => e.IdErrorImportance).HasColumnName("id_error_importance");
            entity.Property(e => e.IdServer).HasColumnName("id_server");
            entity.Property(e => e.MessageProblem).HasColumnName("message_problem");
            entity.Property(e => e.StatusProblem).HasColumnName("status_problem");

            entity.HasOne(d => d.IdErrorImportanceNavigation).WithMany(p => p.Problems)
                .HasForeignKey(d => d.IdErrorImportance)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("problems_id_error_importance_fkey");

            entity.HasOne(d => d.IdServerNavigation).WithMany(p => p.Problems)
                .HasForeignKey(d => d.IdServer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("problems_id_server_fkey");
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.IdServer).HasName("servers_pkey");

            entity.ToTable("servers");

            entity.Property(e => e.IdServer)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id_server");
            entity.Property(e => e.IpAdress)
                .HasColumnType("character varying")
                .HasColumnName("ip_adress");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.NameServer)
                .HasColumnType("character varying")
                .HasColumnName("name_server");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
