using System;
using System.Collections.Generic;

namespace ModuloAdministracion.Models
{
    public partial class DetalleVentum
    {
        public int Id { get; set; }
        public int ProductosId { get; set; }
        public int VentasId { get; set; }

        public virtual Producto Productos { get; set; } = null!;
        public virtual Venta Ventas { get; set; } = null!;
    }
}
