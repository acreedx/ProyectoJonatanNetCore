using System;
using System.Collections.Generic;

namespace ModuloAdministracion.Models
{
    public partial class Detallecompra
    {
        public int Id { get; set; }
        public int ProductosId { get; set; }
        public int ComprasId { get; set; }

        public virtual Compra Compras { get; set; } = null!;
        public virtual Producto Productos { get; set; } = null!;
    }
}
