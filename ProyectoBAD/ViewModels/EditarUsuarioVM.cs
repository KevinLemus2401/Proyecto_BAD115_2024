using System.ComponentModel.DataAnnotations;

namespace ProyectoBAD.ViewModels
{
    public class EditarUsuarioVM
    {
        [EmailAddress]
        [Required(ErrorMessage = "El correo electrónico es requerido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Primer nombre es requerido")]
        public string? PrimerNombre { get; set; }

        public string? SegundoNombre { get; set; }

        [Required(ErrorMessage = "Segundo nombre es requerido")]
        public string? PrimerApellido { get; set; }

        public string? SegundoApellido { get; set; }

        public string? Telefono { get; set; }

    }
}
