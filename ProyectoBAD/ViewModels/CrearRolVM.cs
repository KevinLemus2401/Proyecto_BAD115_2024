using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProyectoBAD.ViewModels
{
    public class CrearRolVM
    {

        public string? IdRol { get; set; }

        [Required(ErrorMessage = "El nombre del rol es requerido.")]
        [Display(Name = "Nombre del Rol")]
        public string? NombreRol { get; set; }

        [Required(ErrorMessage = "Al menos una política debe ser seleccionada.")]
        [Display(Name = "Políticas")]

        public List<string>? Permission { get; set; }

        public List<SelectListItem>? ListPermission {  get; set; }


        [Required(ErrorMessage = "La descripción es requerida")]
        [DataType(DataType.Text)]
        public string? Descripcion { get; set; }
    }
}