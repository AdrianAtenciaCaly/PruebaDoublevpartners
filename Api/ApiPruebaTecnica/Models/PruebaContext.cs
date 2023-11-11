using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiPruebaTecnica.Models;

public partial class PruebaContext : DbContext
{
    public PruebaContext()
    {
    }

    public PruebaContext(DbContextOptions<PruebaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Personas> Personas { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Personas>(entity =>
        {
            entity.HasKey(e => e.Identificador).HasName("PK__Personas__F2374EB1053F4046");

            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombresApellidosConcat)
                .HasMaxLength(101)
                .IsUnicode(false)
                .HasComputedColumnSql("(([Nombres]+' ')+[Apellidos])", false);
            entity.Property(e => e.NumeroIdentificacion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NumeroIdentificacionConcat)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComputedColumnSql("([TipoIdentificacion]+[NumeroIdentificacion])", false);
            entity.Property(e => e.TipoIdentificacion)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.Identificador).HasName("PK__Usuarios__F2374EB13C79D8E5");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Pass)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
