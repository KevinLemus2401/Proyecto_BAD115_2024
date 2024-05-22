using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoBAD.Models;
using System.Security.Claims;

namespace ProyectoBAD.Controllers
{
    [Authorize]
    public class EncuestaController : Controller
    {
        private readonly sisencuestasContext _context;
        private readonly UserManager<Usuario> _userManager;

        public EncuestaController(sisencuestasContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Encuesta
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var userEncuestas = _context.Encuesta
                .Include(e => e.IdUsuarioNavigation)
                .Where(e => e.IdUsuario == user.Id);

            return View(await userEncuestas.ToListAsync());
        }

        // GET: Encuesta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Encuesta == null)
            {
                return NotFound();
            }

            var encuestum = await _context.Encuesta
                .Include(e => e.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdEncuesta == id);
            if (encuestum == null)
            {
                return NotFound();
            }

            return View(encuestum);
        }

        // GET: Encuesta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Encuesta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TituloEncuesta,ObjetivoEncuesta,GrupometaEncuesta,IndicacionesEncuesta")] Encuestum encuestum)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized();
                }

                encuestum.IdUsuario = user.Id;
                encuestum.FechaEncuesta = DateTime.Now;
                encuestum.EstadoEncuesta = true;

                _context.Add(encuestum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(encuestum);
        }


        // GET: Encuesta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encuesta = await _context.Encuesta.FindAsync(id);
            if (encuesta == null)
            {
                return NotFound();
            }

            return View(encuesta);
        }

        // POST: Encuesta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEncuesta,TituloEncuesta,ObjetivoEncuesta,GrupometaEncuesta,IndicacionesEncuesta")] Encuestum encuesta)
        {
            if (id != encuesta.IdEncuesta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Mantener los valores no editables
                    var originalEncuesta = await _context.Encuesta.AsNoTracking().FirstOrDefaultAsync(e => e.IdEncuesta == id);
                    encuesta.IdUsuario = originalEncuesta.IdUsuario;
                    encuesta.FechaEncuesta = originalEncuesta.FechaEncuesta;
                    encuesta.EstadoEncuesta = originalEncuesta.EstadoEncuesta;

                    _context.Update(encuesta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncuestaExists(encuesta.IdEncuesta))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(encuesta);
        }

        private bool EncuestaExists(int id)
        {
            return _context.Encuesta.Any(e => e.IdEncuesta == id);
        }

        // GET: Encuesta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Encuesta == null)
            {
                return NotFound();
            }

            var encuestum = await _context.Encuesta
                .Include(e => e.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdEncuesta == id);
            if (encuestum == null)
            {
                return NotFound();
            }

            return View(encuestum);
        }

        // POST: Encuesta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Encuesta == null)
            {
                return Problem("Entity set 'sisencuestasContext.Encuesta'  is null.");
            }
            var encuestum = await _context.Encuesta.FindAsync(id);
            if (encuestum != null)
            {
                _context.Encuesta.Remove(encuestum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncuestumExists(int id)
        {
            return (_context.Encuesta?.Any(e => e.IdEncuesta == id)).GetValueOrDefault();
        }

        // GET: Encuesta/ShowQuestions/5
        public async Task<IActionResult> ShowQuestions(int? id)
        {
            if (id == null || _context.Encuesta == null)
            {
                return NotFound();
            }

            var encuestum = await _context.Encuesta
                .Include(e => e.Pregunta)  // Incluir las preguntas
                    .ThenInclude(p => p.TipoPregunta)  // Incluir el tipo de pregunta
                .FirstOrDefaultAsync(m => m.IdEncuesta == id);

            if (encuestum == null)
            {
                return NotFound();
            }

            ViewBag.EncuestaTitulo = encuestum.TituloEncuesta;

            // Pasar los datos de las preguntas a la vista de preguntas existente
            return View("~/Views/Preguntums/Index.cshtml", encuestum.Pregunta);
        }
    }
}
