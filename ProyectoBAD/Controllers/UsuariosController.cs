using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoBAD.Models;
using ProyectoBAD.ViewModels;

namespace ProyectoBAD.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly sisencuestasContext _context;

        private readonly UserManager<Usuario> _userManager;

        private readonly RoleManager<Roles> _roleManager;

        public UsuariosController(sisencuestasContext context, UserManager<Usuario> userManager, RoleManager<Roles> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Usuarios
        [Authorize(Policy = "PermissionReadUser")]
        public async Task<IActionResult> Index()
        {
            var usuariosConRoles = new List<(Usuario Usuario, IList<string> Roles)>();

            // Obtener la lista de usuarios junto con sus roles
            var usuarios = await _context.Usuarios.ToListAsync();
            foreach (var usuario in usuarios)
            {
                var roles = await _userManager.GetRolesAsync(usuario);
                usuariosConRoles.Add((usuario, roles));
            }

            // Eliminar el usuario "Admin" de la lista si existe
            var adminUser = await _userManager.FindByNameAsync("admin");
            if (adminUser != null)
            {
                var adminRoles = await _userManager.GetRolesAsync(adminUser);
                usuariosConRoles.RemoveAll(u => u.Usuario.UserName == "admin");
            }

            return View(usuariosConRoles);
        }

        // GET: Usuarios/Details/5
        [Authorize(Policy = "PermissionReadUser")]
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        [Authorize(Policy = "PermissionWriteUser")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "PermissionWriteUser")]
        public async Task<IActionResult> Create(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = new()
                {
                    UserName = model.Name,
                    Email = model.Email,
                    EmailUsuario = model.Email!,
                    PrimerNombreUsuario = model.PrimerNombre!,
                    PrimerApellidoUsuario = model.PrimerApellido!,
                    GenUsuario = model.Genero!,
                };

                var result = await _userManager.CreateAsync(usuario, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }

        // GET: Usuarios/Edit/5
        [Authorize(Policy = "PermissionWriteUser")]
        public async Task<IActionResult> Edit(string? id)
        {
            Dictionary<string, object> modelo = new Dictionary<string, object>();

            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            // Lista de opciones para el select
            List<SelectListItem> opciones = new List<SelectListItem>
            {
                new SelectListItem { Value = "M", Text = "Masculino", Selected = usuario.GenUsuario.Trim().Replace(" ", "") == "M"? true: false },
                new SelectListItem { Value = "F", Text = "Femenino", Selected = usuario.GenUsuario.Trim().Replace(" ", "") == "F"? true: false},
            };

            ViewData["opciones"] = opciones;
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "PermissionWriteUser")]
        public async Task<IActionResult> Edit(string id, string PrimerNombreUsuario, string SegundoNombreUsuario, string PrimerApellidoUsuario, string SegundoApellidoUsuario, string TelefonoUsuario, string GenUsuario)
        {
            // Buscar el usuario por su ID
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            // Actualizar datos
            usuario.PrimerNombreUsuario = PrimerNombreUsuario;
            usuario.SegundoNombreUsuario = SegundoNombreUsuario;
            usuario.PrimerApellidoUsuario = PrimerApellidoUsuario;
            usuario.SegundoApellidoUsuario = SegundoApellidoUsuario;
            usuario.TelefonoUsuario = TelefonoUsuario;
            usuario.GenUsuario = GenUsuario;

            // Guardar los cambios en la base de datos
            var resultado = await _userManager.UpdateAsync(usuario);
            if (resultado.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Si hay errores, mostrar los errores de validación
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(usuario);
            }
        }

        // GET: Usuarios/Delete/5
        [Authorize(Policy = "PermissionDeleteUser")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "PermissionDeleteUser")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'sisencuestasContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /User/AssignRole
        [HttpGet]
        [Authorize(Policy = "PermissionRoleUser")]
        public async Task<IActionResult> AsignarRol(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Obtener todos los roles disponibles
            var allRoles = await _roleManager.Roles.ToListAsync();
            
            // Eliminar rol de la lista si existe
            var rol = await _roleManager.FindByNameAsync("Administrador");
            if (rol != null)
            {
                allRoles.RemoveAll(r => r.Name == "Administrador");
            }

            // Obtener los roles asignados al usuario
            var userRoles = await _userManager.GetRolesAsync(user);

            // Crear una lista de SelectListItem
            var roles = allRoles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name,
                Selected = userRoles.Contains(r.Name) // Verificar si el rol está asignado al usuario
            }).ToList();

            var model = new AsignarRolesVM
            {
                UserId = id,
                Roles = roles
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "PermissionRoleUser")]
        public async Task<IActionResult> AsignarRol(AsignarRolesVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles.ToArray());
            await _userManager.RemoveClaimsAsync(user, userClaims);

            if (model.RoleNames is null)
            {
                return RedirectToAction("Index"); // Redirigir a la acción Index del controlador
            }

            // Asignar los nuevos roles seleccionados al usuario
            foreach (var roleName in model.RoleNames)
            {
                await _userManager.AddToRoleAsync(user, roleName);

                // Obtener el rol
                var role = await _roleManager.FindByNameAsync(roleName);

                // Obtener las Claims del rol
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                // Asignar las Claims del rol al usuario
                foreach (var claim in roleClaims)
                {
                    await _userManager.AddClaimAsync(user, claim);
                }
            }

            return RedirectToAction("Index"); // Redirigir a la acción Index del controlador
        }

        private bool UsuarioExists(string id)
        {
            return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
