﻿using Microsoft.EntityFrameworkCore;

namespace Samples.Model
{
    public class AdventureWorksContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "data source=(localdb)\\mssqllocaldb;initial catalog=AdventureWorks2014;Trusted_Connection=True;MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(
                entity =>
                {
                    entity.ToTable("Customer", "Sales");

                    entity.HasIndex(e => e.AccountNumber)
                        .HasDatabaseName("AK_Customer_AccountNumber")
                        .IsUnique();

                    entity.HasIndex(e => e.TerritoryID)
                        .HasDatabaseName("IX_Customer_TerritoryID");

                    entity.HasIndex(e => e.rowguid)
                        .HasDatabaseName("AK_Customer_rowguid")
                        .IsUnique();

                    entity.Property(e => e.AccountNumber)
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    entity.Property(e => e.ModifiedDate)
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
                });
        }

        public virtual DbSet<Customer> Customers { get; set; }
    }
}
