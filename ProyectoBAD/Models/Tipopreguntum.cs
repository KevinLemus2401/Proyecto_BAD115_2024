using System;
using System.Collections.Generic;

namespace ProyectoBAD.Models
{
    public partial class Tipopreguntum
    {
        public decimal TipoPreguntaId { get; set; }
        public string NombreTipoPregunta { get; set; } = null!;
        public string? DescripcionTipoPregunta { get; set; }
    }
}
