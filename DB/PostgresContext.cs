using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SSHProject;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Parameter> Parameters { get; set; }

    public virtual DbSet<Problem> Problems { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<ServersGroup> ServersGroups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Parameter>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("parameters_pkey");

            entity.ToTable("parameters");

            entity.Property(e => e.RequestId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("request_id");
            entity.Property(e => e.CpuPercent).HasColumnName("cpu_percent");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.IdServer).HasColumnName("id_server");
            entity.Property(e => e.RamMb).HasColumnName("ram_mb");
            entity.Property(e => e.RomMb).HasColumnName("rom_mb");

            entity.HasOne(d => d.IdServerNavigation).WithMany(p => p.Parameters)
                .HasForeignKey(d => d.IdServer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("parameters_id_server_fkey");
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
            entity.Property(e => e.ErrorImportance).HasColumnName("error_importance");
            entity.Property(e => e.IdServer).HasColumnName("id_server");
            entity.Property(e => e.MessageProblem).HasColumnName("message_problem");
            entity.Property(e => e.StatusProblem).HasColumnName("status_problem");

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
            entity.Property(e => e.IdServerGroup).HasColumnName("id_server_group");
            entity.Property(e => e.IpAdress)
                .HasColumnType("character varying")
                .HasColumnName("ip_adress");
            entity.Property(e => e.NameServer)
                .HasColumnType("character varying")
                .HasColumnName("name_server");
            entity.Property(e => e.ServerStatus).HasColumnName("server_status");

            entity.HasOne(d => d.IdServerGroupNavigation).WithMany(p => p.Servers)
                .HasForeignKey(d => d.IdServerGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("servers_id_server_group_fkey");
        });

        modelBuilder.Entity<ServersGroup>(entity =>
        {
            entity.HasKey(e => e.IdServerGroup).HasName("servers_groups_pkey");

            entity.ToTable("servers_groups");

            entity.Property(e => e.IdServerGroup)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id_server_group");
            entity.Property(e => e.NameServerGroup)
                .HasColumnType("character varying")
                .HasColumnName("name_server_group");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
