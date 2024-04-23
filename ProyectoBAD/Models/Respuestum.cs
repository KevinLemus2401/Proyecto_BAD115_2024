using System;
using System.Collections.Generic;

namespace ProyectoBAD.Models
{
    public partial class Respuestum
    {
        public decimal RespuestaId { get; set; }
        public int? OpcionId { get; set; }
        public int? IdPregunta { get; set; }
        public int? IdEncuestado { get; set; }
        public int? IdEncuesta { get; set; }
        public byte[]? FechaRespuesta { get; set; }
        public string? TextoRespuesta { get; set; }
    }
}
