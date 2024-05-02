using System;
using System.Collections.Generic;

namespace ProyectoBAD.Models
{
    public partial class Tipopreguntum
    {
        public Tipopreguntum()
        {
            Pregunta = new HashSet<Preguntum>();
        }

        public int TipoPreguntaId { get; set; }
        public string NombreTipoPregunta { get; set; } = null!;
        public string? DescripcionTipoPregunta { get; set; }

        public virtual ICollection<Preguntum> Pregunta { get; set; }
    }
}
