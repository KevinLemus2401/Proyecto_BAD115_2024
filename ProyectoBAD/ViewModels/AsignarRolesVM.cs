using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoBAD.Models;
using System.ComponentModel.DataAnnotations;


namespace ProyectoBAD.ViewModels
{
    public class AsignarRolesVM
    {
        public string? UserId { get; set; }

        public List<SelectListItem>? Roles { get; set; }

        public List<string>? RoleNames { get; set; }
    }
}
