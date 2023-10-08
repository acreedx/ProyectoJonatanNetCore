using System;
using System.Collections.Generic;

namespace ModuloAdministracion.Models
{
    public partial class Proveedore
    {
        public Proveedore()
        {
            Productos = new HashSet<Producto>();
        }

        public int Id { get; set; }
        public string Proveedor { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public short Estado { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
