using System;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoBAD.Models
{
    public partial class Encuestado
    {
        public Encuestado()
        {
            Respuesta = new HashSet<Respuestum>();
        }

        public int IdEncuestado { get; set; }
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [Display(Name = "Correo Electrónico")]
        public string EmailEncuestado { get; set; } = null!;
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [FechaNoFutura(ErrorMessage = "La fecha de nacimiento no puede ser una fecha futura.")]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacEncuesta { get; set; }
        [Required(ErrorMessage = "El género es obligatorio.")]
        [Display(Name = "Género")]
        public string? GenEncuestado { get; set; }

        public virtual ICollection<Respuestum> Respuesta { get; set; }
    }
}


public class FechaNoFuturaAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            DateTime fechaIngresada = (DateTime)value;
            if (fechaIngresada > DateTime.Now)
            {
                return new ValidationResult("La fecha de nacimiento no puede ser una fecha futura.");
            }
        }
        return ValidationResult.Success;
    }
}