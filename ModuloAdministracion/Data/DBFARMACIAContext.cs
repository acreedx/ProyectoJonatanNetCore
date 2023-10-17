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
        public virtual DbSet<Detallecompra> Detallecompras { get; set; } = null!;
        public virtual DbSet<Detalleventum> Detalleventa { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Proveedore> Proveedores { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Venta> Ventas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=DESKTOP-4ERJMQ6;Database=DBFARMACIA;User Id = sa; Password= 72009919Believer;TrustServerCertificate=True;");
                optionsBuilder.UseSqlServer("Server=DBFARMACIA.mssql.somee.com;Database=DBFARMACIA;User Id= acreedx2_SQLLogin_1; Password=q7pkadcw4l; TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("categorias");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Categoria1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("categoria");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.ToTable("compras");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Fechacompra)
                    .HasColumnType("date")
                    .HasColumnName("fechacompra");

                entity.Property(e => e.Total)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("total");

                entity.Property(e => e.UsuariosId).HasColumnName("usuarios_id");

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.UsuariosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("compras_usuarios_fk");
            });

            modelBuilder.Entity<Detallecompra>(entity =>
            {
                entity.ToTable("detallecompra");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ComprasId).HasColumnName("compras_id");

                entity.Property(e => e.ProductosId).HasColumnName("productos_id");

                entity.HasOne(d => d.Compras)
                    .WithMany(p => p.Detallecompras)
                    .HasForeignKey(d => d.ComprasId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detallecompra_compras_fk");

                entity.HasOne(d => d.Productos)
                    .WithMany(p => p.Detallecompras)
                    .HasForeignKey(d => d.ProductosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detallecompra_productos_fk");
            });

            modelBuilder.Entity<Detalleventum>(entity =>
            {
                entity.ToTable("detalleventa");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProductosId).HasColumnName("productos_id");

                entity.Property(e => e.VentasId).HasColumnName("ventas_id");

                entity.HasOne(d => d.Productos)
                    .WithMany(p => p.Detalleventa)
                    .HasForeignKey(d => d.ProductosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detalleventa_productos_fk");

                entity.HasOne(d => d.Ventas)
                    .WithMany(p => p.Detalleventa)
                    .HasForeignKey(d => d.VentasId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detalleventa_ventas_fk");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("productos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CategoriasId).HasColumnName("categorias_id");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombreproducto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombreproducto");
                
                entity.Property(e => e.RutaImagen)
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .HasColumnName("rutaimagen")
                   .IsRequired(false);

                entity.Property(e => e.Precio)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("precio");

                entity.Property(e => e.ProveedoresId).HasColumnName("proveedores_id");

                entity.HasOne(d => d.Categorias)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.CategoriasId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productos_categorias_fk");

                entity.HasOne(d => d.Proveedores)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.ProveedoresId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productos_proveedores_fk");
            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.ToTable("proveedores");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("proveedor");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Rol)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("rol");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.RolesId).HasColumnName("roles_id");

                entity.HasOne(d => d.Roles)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.RolesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usuarios_roles_fk");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("ventas");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Fechaventa)
                    .HasColumnType("date")
                    .HasColumnName("fechaventa");

                entity.Property(e => e.Preciounitario)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("preciounitario");

                entity.Property(e => e.Total)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("total");

                entity.Property(e => e.UsuariosId).HasColumnName("usuarios_id");

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.UsuariosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ventas_usuarios_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
