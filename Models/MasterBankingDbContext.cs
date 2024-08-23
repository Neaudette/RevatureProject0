using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Revature_Project_1.Models;

public partial class MasterBankingDbContext : DbContext
{
    public MasterBankingDbContext()
    {
    }

    public MasterBankingDbContext(DbContextOptions<MasterBankingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountList> AccountLists { get; set; }

    public virtual DbSet<ClientList> ClientLists { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("server=NICS-PC\\NICKYSERVER;database=MasterBankingDB;integrated security=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountList>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__AccountL__349DA586F6C024B7");

            entity.ToTable("AccountList");

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.AccountName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.IsOpen).HasDefaultValue(true);

            entity.HasOne(d => d.Client).WithMany(p => p.AccountLists)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountLi__Clien__286302EC");
        });

        modelBuilder.Entity<ClientList>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__ClientLi__E67E1A04F4125B41");

            entity.ToTable("ClientList");

            entity.Property(e => e.ClientId)
                .ValueGeneratedNever()
                .HasColumnName("ClientID");
            entity.Property(e => e.ClientName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ClientPassword)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsAdministrator).HasColumnName("isAdministrator");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A4B6B98AD79");

            entity.Property(e => e.TransactionId)
                .ValueGeneratedNever()
                .HasColumnName("TransactionID");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Account1Navigation).WithMany(p => p.TransactionAccount1Navigations)
                .HasForeignKey(d => d.Account1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__Accou__2C3393D0");

            entity.HasOne(d => d.Account2Navigation).WithMany(p => p.TransactionAccount2Navigations)
                .HasForeignKey(d => d.Account2)
                .HasConstraintName("FK__Transacti__Accou__2D27B809");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
