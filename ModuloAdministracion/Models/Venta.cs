using System;
using System.Collections.Generic;

namespace ModuloAdministracion.Models
{
    public partial class Venta
    {
        public Venta()
        {
            Detalleventa = new HashSet<Detalleventum>();
        }

        public int Id { get; set; }
        public DateTime Fechaventa { get; set; }
        public int Cantidad { get; set; }
        public decimal Preciounitario { get; set; }
        public decimal Total { get; set; }
        public int UsuariosId { get; set; }

        public virtual Usuario Usuarios { get; set; } = null!;
        public virtual ICollection<Detalleventum> Detalleventa { get; set; }
    }
}
