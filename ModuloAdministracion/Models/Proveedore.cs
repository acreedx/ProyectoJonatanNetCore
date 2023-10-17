using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModuloAdministracion.Models
{
    public partial class Proveedore
    {
        public Proveedore()
        {
            Productos = new HashSet<Producto>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre del proveedor")]
        public string Proveedor { get; set; } = null!;
        [Required(ErrorMessage = "Ingrese el telefono del proveedor")]
        public string Telefono { get; set; } = null!;
        public short Estado { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
