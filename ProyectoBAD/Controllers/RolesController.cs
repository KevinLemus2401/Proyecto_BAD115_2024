using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoBAD.Models;
using ProyectoBAD.ViewModels;
using System.Security.Claims;

namespace ProyectoBAD.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<Models.Roles> _roleManager;

        public RolesController(RoleManager<Models.Roles> roleManager)
        {
            _roleManager = roleManager;
        }

        [Authorize(Policy = "PermissionReadRole")]
        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles.ToList();

            // Eliminar rol de la lista si existe
            var rol = await _roleManager.FindByNameAsync("Administrador");
            if (rol != null)
            {
                roles.RemoveAll(r => r.Name == "Administrador");
            }
            return View(roles);
        }

        [HttpGet]
        [Authorize(Policy = "PermissionWriteRole")]
        public IActionResult CrearRol()
        {
            var model = new CrearRolVM() { ListPermission = permission() };
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "PermissionWriteRole")]
        public async Task<IActionResult> CrearRol(CrearRolVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var nuevoRol = new Models.Roles { Name = model.NombreRol, Descripcion = model.Descripcion };
            var resultado = await _roleManager.CreateAsync(nuevoRol);

            if (resultado.Succeeded)
            {
                var rol = await _roleManager.FindByNameAsync(model.NombreRol);
                await AgregarReclamacionesAsync(rol!, model.Permission!);
                return RedirectToAction("Index"); // Redirigir a la página de lista de roles
            }

            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            model.ListPermission = permission();
            return View(model);
        }

        [HttpGet]
        [Authorize(Policy = "PermissionWriteRole")]
        public IActionResult ActualizarRol(string id)
        {
            if (id == null) return NotFound();

            var role = _roleManager.FindByIdAsync(id).Result;
            if (role == null) return NotFound();

            var permissionRoles = _roleManager.GetClaimsAsync(role).Result;
            var listpermission = permission();

            foreach (var item in listpermission) 
            {
                if (permissionRoles.Any(c => c.Type == "Permission" && c.Value == item.Value))
                {
                    item.Selected = true;
                }
            }

            CrearRolVM model = new CrearRolVM() { IdRol = role.Id, NombreRol = role.Name, Descripcion = role.Descripcion, ListPermission = listpermission };
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "PermissionWriteRole")]
        public async Task<IActionResult> ActualizarRol(CrearRolVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var rol = await _roleManager.FindByIdAsync(model.IdRol);
            if (rol == null)
            {
                return NotFound();
            }

            rol.Name = model.NombreRol;
            rol.Descripcion = model.Descripcion;

            var result = await _roleManager.UpdateAsync(rol);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                model.ListPermission = permission();
                return View(model);
            }

            await AgregarReclamacionesAsync(rol, model.Permission!);

            return RedirectToAction("Index"); // Redirigir a la página de lista de roles
        }


        [HttpGet]
        [Authorize(Policy = "PermissionDeleteRole")]
        public IActionResult EliminarRol(string id)
        {
            var rol = _roleManager.Roles.FirstOrDefault(r => r.Id == id);
            if (rol == null)
            {
                ViewBag.ErrorMessage = $"El rol con ID '{id}' no fue encontrado.";
                return View("Error");
            }
            return View(rol);
        }

        [HttpPost, ActionName("EliminarRol") ]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "PermissionDeleteRole")]
        public async Task<IActionResult> ConfirmarEliminarRol(string id)
        {
            var rol = await _roleManager.FindByIdAsync(id);
            if (rol == null)
            {
                ViewBag.ErrorMessage = $"El rol con ID '{id}' no fue encontrado.";
                return View("Error");
            }

            var resultado = await _roleManager.DeleteAsync(rol);
            if (resultado.Succeeded)
            {
                return RedirectToAction("Index"); // Redirigir a la página de lista de roles
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View("EliminarRol", rol);
            }
        }

        private async Task AgregarReclamacionesAsync(Roles rol, IEnumerable<string> permissions)
        {
            // Obtener todas las reclamaciones del rol
            var roleClaims = await _roleManager.GetClaimsAsync(rol);

            // Eliminar todas las reclamaciones del rol
            foreach (var claim in roleClaims)
            {
                await _roleManager.RemoveClaimAsync(rol, claim);
            }

            // Agregar nuevas reclamaciones al rol
            foreach (var permission in permissions)
            {
                var claimResult = await _roleManager.AddClaimAsync(rol, new Claim("Permission", permission));

                if (!claimResult.Succeeded)
                {
                    throw new Exception("Error al agregar el claim");
                }
            }
        }

        //Lista de reclamaciones
        public List<SelectListItem> permission()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Leer:Usuario", Value = "ReadUser" },
                new SelectListItem { Text = "Escribir:Usuario", Value = "WriteUser" },
                new SelectListItem { Text = "Eliminar:Usuario", Value = "DeleteUser" },
                new SelectListItem { Text = "Roles:Usuario", Value = "UserRoles" },
                new SelectListItem { Text = "Leer:Roles", Value = "ReadRole" },
                new SelectListItem { Text = "Escribir:Roles", Value = "WriteRole" },
                new SelectListItem { Text = "Eliminar:Roles", Value = "DeleteRole" }
            };
        }
    }
}