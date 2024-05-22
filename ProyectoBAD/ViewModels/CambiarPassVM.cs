using System.ComponentModel.DataAnnotations;

namespace ProyectoBAD.ViewModels
{
    public class CambiarPassVM
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string ContraseñaActual { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string NuevaContraseña { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nueva Contraseña")]
        [Compare("NuevaContraseña", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarNuevaContraseña { get; set; }
    }
}
