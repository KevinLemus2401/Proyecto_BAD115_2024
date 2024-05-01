using System;
using System.Collections.Generic;

namespace ProyectoBAD.Models
{
    public partial class Preguntum
    {
        public Preguntum()
        {
            Opcionpregunta = new HashSet<Opcionpreguntum>();
            Respuesta = new HashSet<Respuestum>();
        }

        public int IdPregunta { get; set; }
        public int? IdEncuesta { get; set; }
        public int? TipoPreguntaId { get; set; }
        public string DescripcionPregunta { get; set; } = null!;
        public bool? RequeridaPregunta { get; set; }
        public int OrdenPregunta { get; set; }

        public virtual Encuestum? IdEncuestaNavigation { get; set; }
        public virtual Tipopreguntum? TipoPregunta { get; set; }
        public virtual ICollection<Opcionpreguntum> Opcionpregunta { get; set; }
        public virtual ICollection<Respuestum> Respuesta { get; set; }
    }
}
