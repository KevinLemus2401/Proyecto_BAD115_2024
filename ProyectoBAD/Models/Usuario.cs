using System;
using System.Collections.Generic;

namespace ProyectoBAD.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Encuesta = new HashSet<Encuestum>();
        }

        public int IdUsuario { get; set; }
        public string EmailUsuario { get; set; } = null!;
        public string? TelefonoUsuario { get; set; }
        public string PrimerNombreUsuario { get; set; } = null!;
        public string? SegundoNombreUsuario { get; set; }
        public string PrimerApellidoUsuario { get; set; } = null!;
        public string? SegundoApellidoUsuario { get; set; }
        public string GenUsuario { get; set; } = null!;

        public virtual ICollection<Encuestum> Encuesta { get; set; }
    }
}
