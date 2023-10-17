using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModuloAdministracion.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Detallecompras = new HashSet<Detallecompra>();
            Detalleventa = new HashSet<Detalleventum>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre del producto")]
        public string Nombreproducto { get; set; } = null!;
        [Required(ErrorMessage = "Ingrese el precio producto")]
        public decimal Precio { get; set; }
        [Required(ErrorMessage = "Ingrese la cantidad del producto")]
        public int Cantidad { get; set; }
        [Required(ErrorMessage = "Ingrese una imagen para el producto")]
        public string RutaImagen { get; set; }
        public short Estado { get; set; }
        public int CategoriasId { get; set; }
        public int ProveedoresId { get; set; }

        public virtual Categoria Categorias { get; set; } = null!;
        public virtual Proveedore Proveedores { get; set; } = null!;
        public virtual ICollection<Detallecompra> Detallecompras { get; set; }
        public virtual ICollection<Detalleventum> Detalleventa { get; set; }
    }
}
