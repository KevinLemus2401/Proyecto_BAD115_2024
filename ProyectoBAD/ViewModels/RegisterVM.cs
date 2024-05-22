using System.ComponentModel.DataAnnotations;

namespace ProyectoBAD.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string? PrimerNombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        public string? PrimerApellido { get; set; }

        [Required(ErrorMessage = "El genero es requerido")]
        public string? Genero { get; set; }

        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "La contraseña con coinciden")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        public string? ConfirmPassword { get; set; }


    }
}
