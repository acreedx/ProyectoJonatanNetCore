using System;
using System.Collections.Generic;

namespace ModuloAdministracion.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Compras = new HashSet<Compra>();
            Venta = new HashSet<Venta>();
        }

        public int Id { get; set; }
        public string Correo { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int RolesId { get; set; }
        public short Estado { get; set; }

        public virtual Role Roles { get; set; } = null!;
        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
