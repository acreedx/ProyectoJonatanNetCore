using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Ingrese el correo del usuario")]
        public string Correo { get; set; } = null!;
        [Required(ErrorMessage = "Ingrese el password del usuario")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Ingrese el nombre del usuario")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "Ingrese el apellido del usuario")]
        public string Apellido { get; set; } = null!;
        public int RolesId { get; set; }
        public short Estado { get; set; }

        public virtual Role Roles { get; set; } = null!;
        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
