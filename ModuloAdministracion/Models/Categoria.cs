using System;
using System.Collections.Generic;

namespace ModuloAdministracion.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public int Id { get; set; }
        public string Categoria1 { get; set; } = null!;

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
