using System;
using System.Collections.Generic;

namespace ModuloAdministracion.Models
{
    public partial class Compra
    {
        public Compra()
        {
            Detallecompras = new HashSet<Detallecompra>();
        }

        public int Id { get; set; }
        public DateTime Fechacompra { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public int UsuariosId { get; set; }

        public virtual Usuario Usuarios { get; set; } = null!;
        public virtual ICollection<Detallecompra> Detallecompras { get; set; }
    }
}
