﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SampleAPI.DAL.Models;

namespace SampleAPI.DAL.DbContexts
{
    public partial class SampleAPIContextBase : DbContext
    {
        public SampleAPIContextBase()
        {
        }

        public SampleAPIContextBase(DbContextOptions<SampleAPIContextBase> options)
            : base(options)
        {
        }

        public virtual DbSet<APIAccessLog> APIAccessLog { get; set; }
        public virtual DbSet<APIProfile> APIProfile { get; set; }
        public virtual DbSet<APIProfileService> APIProfileService { get; set; }
        public virtual DbSet<APIService> APIService { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=kserver;Initial Catalog=SampleAPI;Persist Security Info=True;User ID=kserver_sql_dev;Password=kserver_sql_dev;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<APIAccessLog>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IPAddress)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.APIProfileService)
                    .WithMany(p => p.APIAccessLog)
                    .HasForeignKey(d => d.APIProfileServiceId)
                    .HasConstraintName("FK_APIAccessLog_APIProfileService");
            });

            modelBuilder.Entity<APIProfile>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<APIProfileService>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ServiceDefinedFields).IsUnicode(false);

                entity.HasOne(d => d.APIProfile)
                    .WithMany(p => p.APIProfileService)
                    .HasForeignKey(d => d.APIProfileId)
                    .HasConstraintName("FK_APIProfileService_APIProfile");

                entity.HasOne(d => d.APIService)
                    .WithMany(p => p.APIProfileService)
                    .HasForeignKey(d => d.APIServiceId)
                    .HasConstraintName("FK_APIProfileService_APIService");
            });

            modelBuilder.Entity<APIService>(entity =>
            {
                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ConnectionInfo).IsUnicode(false);

                entity.Property(e => e.Controller)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.DisabledResponseMessage)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceDefinedFields).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
