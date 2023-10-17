using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModuloAdministracion.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre de categoria")]
        public string Categoria1 { get; set; } = null!;

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
