using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using coba.Models;

namespace coba.Data;

public partial class bapendaContext : DbContext
{
    public bapendaContext()
    {
    }

    public bapendaContext(DbContextOptions<bapendaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Pegawai> Pegawais { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=bapenda;Username=postgres;Password=dikisatria12");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.IdAdmin).HasName("admin_pkey");

            entity.ToTable("admin");

            entity.Property(e => e.IdAdmin)
                .ValueGeneratedNever()
                .HasColumnName("id_admin");
            entity.Property(e => e.NamaAdmin)
                .HasMaxLength(100)
                .HasColumnName("nama_admin");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Pegawai>(entity =>
        {
            entity.HasKey(e => e.IdPegawai).HasName("pegawai_pkey");

            entity.ToTable("pegawai");
            entity.Property(e => e.IdPegawai)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_pegawai");
            entity.Property(e => e.Jabatan)
                .HasMaxLength(50)
                .HasColumnName("jabatan");
            entity.Property(e => e.NamaPegawai)
                .HasMaxLength(100)
                .HasColumnName("nama_pegawai");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
