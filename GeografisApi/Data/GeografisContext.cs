using System;
using System.Collections.Generic;
using GeografisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GeografisApi.Data;

public partial class GeografisContext : DbContext
{
    public GeografisContext()
    {
    }

    public GeografisContext(DbContextOptions<GeografisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Kategori> Kategoris { get; set; }
    public virtual DbSet<Umkm> Umkms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=\"localhost,port=3306\";user=root;database=geografis", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
