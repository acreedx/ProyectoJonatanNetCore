using System;
using System.Collections.Generic;

namespace ModuloAdministracion.Models
{
    public partial class Compra
    {
        public Compra()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
        }

        public int Id { get; set; }
        public DateTime FechaCompra { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public int UsuariosId { get; set; }

        public virtual Usuario Usuarios { get; set; } = null!;
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
    }
}
