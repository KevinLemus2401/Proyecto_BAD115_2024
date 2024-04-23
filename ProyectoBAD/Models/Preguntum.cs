using System;
using System.Collections.Generic;

namespace ProyectoBAD.Models
{
    public partial class Preguntum
    {
        public decimal IdPregunta { get; set; }
        public int? IdEncuesta { get; set; }
        public int? TipoPreguntaId { get; set; }
        public string DescripcionPregunta { get; set; } = null!;
        public bool? RequeridaPregunta { get; set; }
        public int OrdenPregunta { get; set; }
    }
}
