using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ModuloAdministracion.Models;

namespace ModuloAdministracion.Data
{
    public partial class DBFARMACIAContext : DbContext
    {
        public DBFARMACIAContext()
        {
        }

        public DBFARMACIAContext(DbContextOptions<DBFARMACIAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<DetalleCompra> DetalleCompras { get; set; } = null!;
        public virtual DbSet<DetalleVentum> DetalleVenta { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Proveedore> Proveedores { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Venta> Ventas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-4ERJMQ6;Database=DBFARMACIA;User Id= sa; Password=72009919Believer; TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Categoria1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Categoria");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FechaCompra).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.UsuariosId).HasColumnName("Usuarios_Id");

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.UsuariosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Compras_Usuarios_FK");
            });

            modelBuilder.Entity<DetalleCompra>(entity =>
            {
                entity.ToTable("DetalleCompra");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ComprasId).HasColumnName("Compras_Id");

                entity.Property(e => e.ProductosId).HasColumnName("Productos_Id");

                entity.HasOne(d => d.Compras)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.ComprasId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DetalleCompra_Compras_FK");

                entity.HasOne(d => d.Productos)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.ProductosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DetalleCompra_Productos_FK");
            });

            modelBuilder.Entity<DetalleVentum>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ProductosId).HasColumnName("Productos_Id");

                entity.Property(e => e.VentasId).HasColumnName("Ventas_Id");

                entity.HasOne(d => d.Productos)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.ProductosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DetalleVenta_Productos_FK");

                entity.HasOne(d => d.Ventas)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.VentasId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DetalleVenta_Ventas_FK");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CategoriasId).HasColumnName("Categorias_Id");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.ProveedoresId).HasColumnName("Proveedores_Id");

                entity.HasOne(d => d.Categorias)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.CategoriasId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Productos_Categorias_FK");

                entity.HasOne(d => d.Proveedores)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.ProveedoresId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Productos_Proveedores_FK");
            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Rol)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RolesId).HasColumnName("Roles_Id");

                entity.HasOne(d => d.Roles)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.RolesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Usuarios_Roles_FK");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FechaVenta).HasColumnType("datetime");

                entity.Property(e => e.PrecioUnitario).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Total).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.UsuariosId).HasColumnName("Usuarios_Id");

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.UsuariosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ventas_Usuarios_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
