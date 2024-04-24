using System;
using System.Collections.Generic;

namespace ProyectoBAD.Models
{
    public partial class Encuestado
    {
        public decimal IdEncuestado { get; set; }
        public string EmailEncuestado { get; set; } = null!;
        public DateTime FechaNacEncuesta { get; set; }
        public string? GenEncuestado { get; set; }
    }
}
