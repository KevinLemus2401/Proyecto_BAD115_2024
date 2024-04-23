using System;
using System.Collections.Generic;

namespace ProyectoBAD.Models
{
    public partial class Encuestum
    {
        public decimal IdEncuesta { get; set; }
        public int? IdUsuario { get; set; }
        public string TituloEncuesta { get; set; } = null!;
        public string ObjetivoEncuesta { get; set; } = null!;
        public string GrupometaEncuesta { get; set; } = null!;
        public string IndicacionesEncuesta { get; set; } = null!;
        public byte[] FechaEncuesta { get; set; } = null!;
        public bool? EstadoEncuesta { get; set; }
    }
}
