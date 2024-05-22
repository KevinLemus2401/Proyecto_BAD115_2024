using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProyectoBAD.Models
{
    public class Roles : IdentityRole
    {
        public Roles() : base()
        {

        }
        public Roles(string roleName, string Descrption) : base(roleName)
        {
            this.Descripcion = Descrption;
        }

        [Required]
        public string? Descripcion { get ; set; }
    }
}
