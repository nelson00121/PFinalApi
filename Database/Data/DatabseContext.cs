using System;
using System.Collections.Generic;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data;

public partial class DatabseContext : DbContext
{
    public DatabseContext()
    {
    }

    public DatabseContext(DbContextOptions<DatabseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alimento> Alimentos { get; set; }

    public virtual DbSet<Animale> Animales { get; set; }

    public virtual DbSet<Archivo> Archivos { get; set; }

    public virtual DbSet<BitacoraAlimento> BitacoraAlimentos { get; set; }

    public virtual DbSet<BitacoraPeso> BitacoraPesos { get; set; }

    public virtual DbSet<BitacorasVacuna> BitacorasVacunas { get; set; }

    public virtual DbSet<CompraAlimento> CompraAlimentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EstadosAnimale> EstadosAnimales { get; set; }

    public virtual DbSet<HistorialAlimenticio> HistorialAlimenticios { get; set; }

    public virtual DbSet<Raza> Razas { get; set; }

    public virtual DbSet<Reporte> Reportes { get; set; }

    public virtual DbSet<TiposAlimento> TiposAlimentos { get; set; }

    public virtual DbSet<UnidadesDeMedidaAlimento> UnidadesDeMedidaAlimentos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vacuna> Vacunas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=interchange.proxy.rlwy.net;Port=49952;Database=railway;Uid=root;Pwd=aRcZJTlisNyoJXrKRLmaQceAwtQgGfiw;");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alimento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("'current_timestamp()'");
            entity.Property(e => e.Nombre).HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.TipoAlimento).WithMany(p => p.Alimentos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("alimentos_ibfk_1");

            entity.HasOne(d => d.UnidadesDeMedidaAlimentos).WithMany(p => p.Alimentos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("alimentos_ibfk_2");
        });

        modelBuilder.Entity<Animale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("'current_timestamp()'");

            entity.HasOne(d => d.EstadoAnimal).WithMany(p => p.Animales)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("animales_ibfk_2");

            entity.HasOne(d => d.Raza).WithMany(p => p.Animales)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("animales_ibfk_1");
        });

        modelBuilder.Entity<Archivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.Reporte).WithMany(p => p.Archivos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("archivo_ibfk_1");
        });

        modelBuilder.Entity<BitacoraAlimento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("'current_timestamp()'");

            entity.HasOne(d => d.Alimento).WithMany(p => p.BitacoraAlimentos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("bitacora_alimento_ibfk_1");
        });

        modelBuilder.Entity<BitacoraPeso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("'current_timestamp()'");

            entity.HasOne(d => d.Animal).WithMany(p => p.BitacoraPesos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("bitacora_pesos_ibfk_1");
        });

        modelBuilder.Entity<BitacorasVacuna>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("'current_timestamp()'");

            entity.HasOne(d => d.Animal).WithMany(p => p.BitacorasVacunas)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("bitacoras_vacunas_ibfk_1");

            entity.HasOne(d => d.Vacuna).WithMany(p => p.BitacorasVacunas)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("bitacoras_vacunas_ibfk_2");
        });

        modelBuilder.Entity<CompraAlimento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.AlimentoId).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CantidadComprada).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("'current_timestamp()'");

            entity.HasOne(d => d.Alimento).WithMany(p => p.CompraAlimentos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("compra_alimentos_ibfk_1");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("'current_timestamp()'");
            entity.Property(e => e.SegundoApellido).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.SegundoNombre).HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<EstadosAnimale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<HistorialAlimenticio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("'current_timestamp()'");

            entity.HasOne(d => d.Alimento).WithMany(p => p.HistorialAlimenticios)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("historial_alimenticio_ibfk_3");

            entity.HasOne(d => d.Animal).WithMany(p => p.HistorialAlimenticios)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("historial_alimenticio_ibfk_1");

            entity.HasOne(d => d.Empleado).WithMany(p => p.HistorialAlimenticios)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("historial_alimenticio_ibfk_2");
        });

        modelBuilder.Entity<Raza>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("'current_timestamp()'");

            entity.HasOne(d => d.Animal).WithMany(p => p.Reportes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("reportes_ibfk_1");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Reportes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("reportes_ibfk_2");
        });

        modelBuilder.Entity<TiposAlimento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Descripcion).HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<UnidadesDeMedidaAlimento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("'current_timestamp()'");

            entity.HasOne(d => d.Empleado).WithOne(p => p.Usuario)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("usuarios_ibfk_1");
        });

        modelBuilder.Entity<Vacuna>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Descripcion).HasDefaultValueSql("'NULL'");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
