using System;
using System.Collections.Generic;

namespace ProyectoBAD.Models
{
    public partial class Encuestado
    {
        public Encuestado()
        {
            Respuesta = new HashSet<Respuestum>();
        }

        public int IdEncuestado { get; set; }
        public string EmailEncuestado { get; set; } = null!;
        public DateTime FechaNacEncuesta { get; set; }
        public string? GenEncuestado { get; set; }

        public virtual ICollection<Respuestum> Respuesta { get; set; }
    }
}
