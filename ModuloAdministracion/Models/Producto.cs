using System;
using System.Collections.Generic;

namespace ModuloAdministracion.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        public int Id { get; set; }
        public string NombreProducto { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public short Estado { get; set; }
        public int CategoriasId { get; set; }
        public int ProveedoresId { get; set; }

        public virtual Categoria Categorias { get; set; } = null!;
        public virtual Proveedore Proveedores { get; set; } = null!;
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
