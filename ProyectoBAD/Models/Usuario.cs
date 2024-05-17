using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace ProyectoBAD.Models
{
    public partial class Usuario : IdentityUser
    {
        public Usuario()
        {
            Encuesta = new HashSet<Encuestum>();
        }

        public Usuario(string EmailUsuario, string PrimerNombreUsuario, string PrimerApellidoUsuario, string GenUsuario) : base()
        {
            
            this.EmailUsuario = EmailUsuario;
            this.PrimerNombreUsuario = PrimerNombreUsuario;
            this.PrimerApellidoUsuario = PrimerApellidoUsuario;
            this.GenUsuario = GenUsuario;

            Encuesta = new HashSet<Encuestum>();
        }

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
