using System.ComponentModel.DataAnnotations;

namespace ProyectoBAD.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Usuario es requerido")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Contraseña es requerido")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name ="Guardar sesión")]
        public bool RememberMe { get; set;}
    }
}
