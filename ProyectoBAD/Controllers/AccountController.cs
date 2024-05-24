using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoBAD.Models;
using ProyectoBAD.ViewModels;

namespace ProyectoBAD.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;

        public AccountController(SignInManager<Usuario> signInManager, UserManager<Usuario> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password!, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Credenciales no validas");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
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
                    await _signInManager.SignInAsync(usuario, false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CambiarPass()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CambiarPass(CambiarPassVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"No se pudo cargar al usuario con ID '{_userManager.GetUserId(User)}'.");
                }

                var result = await _userManager.ChangePasswordAsync(user, model.ContraseñaActual, model.NuevaContraseña);
                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            EditarUsuarioVM model = new EditarUsuarioVM()
            {
                UserName = user.UserName,
                Email = user.Email,
                PrimerNombre = user.PrimerNombreUsuario,
                SegundoNombre = user.SegundoNombreUsuario,
                PrimerApellido = user.PrimerApellidoUsuario,
                SegundoApellido = user.SegundoApellidoUsuario,
                Telefono = user.TelefonoUsuario,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Settings(EditarUsuarioVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized();
                }

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PrimerNombreUsuario = model.PrimerNombre!;
                user.SegundoNombreUsuario = model.SegundoNombre;
                user.PrimerApellidoUsuario = model.PrimerApellido!;
                user.SegundoApellidoUsuario = model.SegundoApellido;
                user.EmailUsuario = model.Email!;
                user.TelefonoUsuario = model.Telefono;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
    }

}
