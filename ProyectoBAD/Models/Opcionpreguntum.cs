using System;
using System.Collections.Generic;

namespace ProyectoBAD.Models
{
    public partial class Opcionpreguntum
    {
        public decimal OpcionId { get; set; }
        public int? IdPregunta { get; set; }
        public string ValorOpcion { get; set; } = null!;
        public string? DescripcionOpcion { get; set; }
        public bool? EstadoOpcion { get; set; }
        public int? OrdenOpcion { get; set; }
    }
}
