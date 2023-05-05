using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Labb2.Models;

public partial class BokhandelContext : DbContext
{
    public BokhandelContext()
    {
    }

    public BokhandelContext(DbContextOptions<BokhandelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Butiker> Butiker { get; set; }

    public virtual DbSet<Böcker> Böcker { get; set; }

    public virtual DbSet<Författare> Författare { get; set; }

    public virtual DbSet<LagerSaldo> LagerSaldo { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Ordrar> Ordrar { get; set; }

    public virtual DbSet<TitlarPerFörfattare> TitlarPerFörfattare { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-8N6NJ6A7;Initial Catalog=bokhandel;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Butiker>(entity =>
        {
            entity.HasKey(e => e.ButiksId).HasName("PK__Butiker__2BE6E798DC316927");

            entity.ToTable("Butiker");
        });

        modelBuilder.Entity<Böcker>(entity =>
        {
            entity.HasKey(e => e.Isbn13).HasName("PK__Böcker__3BF79E03923E8C5B");

            entity.ToTable("Böcker");

            entity.Property(e => e.Isbn13)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("ISBN13");
            entity.Property(e => e.FörfattareId).HasColumnName("FörfattareID");
            entity.Property(e => e.Språk).HasMaxLength(20);
            entity.Property(e => e.Utgivningsdatum).HasColumnType("date");

            entity.HasOne(d => d.Författare).WithMany(p => p.Böckers)
                .HasForeignKey(d => d.FörfattareId)
                .HasConstraintName("FK__Böcker__Författa__267ABA7A");
        });

        modelBuilder.Entity<Författare>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Författa__3214EC07E8C022A4");

            entity.ToTable("Författare");

            entity.Property(e => e.Födelsedatum).HasColumnType("date");
        });

        modelBuilder.Entity<LagerSaldo>(entity =>
        {
            entity.HasKey(e => new { e.ButiksId, e.Isbn }).HasName("ButiksID_ISBN");

            entity.ToTable("LagerSaldo");

            entity.Property(e => e.Isbn)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("ISBN");

            entity.HasOne(d => d.Butiks).WithMany(p => p.LagerSaldos)
                .HasForeignKey(d => d.ButiksId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LagerSald__Butik__2B3F6F97");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.LagerSaldos)
                .HasForeignKey(d => d.Isbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LagerSaldo__ISBN__2C3393D0");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__OrderDet__C3905BCF0BE687BF");

            entity.Property(e => e.Datum).HasColumnType("date");

            entity.HasOne(d => d.Butiks).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ButiksId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Butik__2F10007B");
        });

        modelBuilder.Entity<Ordrar>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.Isbn }).HasName("PK__Ordrar__67D788A1C47C9D69");

            entity.ToTable("Ordrar");

            entity.Property(e => e.Isbn)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("ISBN");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.Ordrars)
                .HasForeignKey(d => d.Isbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ordrar__ISBN__32E0915F");

            entity.HasOne(d => d.Order).WithMany(p => p.Ordrars)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ordrar__OrderId__31EC6D26");
        });

        modelBuilder.Entity<TitlarPerFörfattare>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TitlarPerFörfattare");

            entity.Property(e => e.Lagervärde)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Titlar)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Ålder)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
