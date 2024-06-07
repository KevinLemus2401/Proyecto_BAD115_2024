using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoBAD.Models;
using Microsoft.AspNetCore.Http.Extensions;
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

            // Pasar el ID de la encuesta a la vista utilizando ViewBag
            ViewBag.EncuestaId = id;

            ViewBag.EncuestaTitulo = encuestum.TituloEncuesta;

            // Pasar los datos de las preguntas a la vista de preguntas existente
            return View("~/Views/Preguntums/Index.cshtml", encuestum.Pregunta);
        }

        public async Task<IActionResult> GenerateLink(int id) 
        {
            var encuesta = await _context.Encuesta.FindAsync(id);
            if (encuesta == null)
            {
                return NotFound();
            }

            var link = Url.Action("StartSurvey", "Encuesta", new { id = id }, protocol: HttpContext.Request.Scheme);
            ViewBag.Link = link;
            return View("ShareLink", encuesta);
        }

        // Acción para mostrar el formulario de datos del encuestado
        [HttpGet]
        public IActionResult StartSurvey(int id)
        {
            var encuestado = new Encuestado();
            ViewBag.IdEncuesta = id;

            // Aquí necesitamos cargar las preguntas de la encuesta
            var preguntas = _context.Pregunta
                .Include(p => p.Opcionpregunta)
                .Where(p => p.IdEncuesta == id)
                .ToList();

            // Pasar las preguntas a la vista
            ViewBag.Preguntas = preguntas;

            return View("SurveyIntro", encuestado);
        }


        // Acción para procesar el formulario de datos del encuestado y redirigir a la encuesta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartSurvey(int id, [Bind("EmailEncuestado,FechaNacEncuesta,GenEncuestado")] Encuestado encuestado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(encuestado);
                await _context.SaveChangesAsync();
                return RedirectToAction("TakeSurvey", new { idEncuesta = id, idEncuestado = encuestado.IdEncuestado });
            }
            ViewBag.IdEncuesta = id;
            return View("SurveyIntro", encuestado);
        }

        public async Task<IActionResult> TakeSurvey(int idEncuesta, int idEncuestado)
        {
            var preguntas = await _context.Pregunta
                .Include(p => p.Opcionpregunta)
                .Where(p => p.IdEncuesta == idEncuesta)
                .ToListAsync();

            ViewBag.IdEncuesta = idEncuesta;
            ViewBag.IdEncuestado = idEncuestado;

            return View("TakeSurvey", preguntas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TakeSurvey(int idEncuesta, int idEncuestado, List<Respuestum> respuestas)
        {
            if (ModelState.IsValid)
            {
                foreach (var respuesta in respuestas)
                {
                    respuesta.IdEncuesta = idEncuesta;
                    respuesta.IdEncuestado = idEncuestado;
                    respuesta.FechaRespuesta = DateTime.Now; // Establecer la fecha actual
                    _context.Add(respuesta);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("SurveyCompleted");
            }

            var preguntas = await _context.Pregunta
                .Include(p => p.Opcionpregunta)
                .Where(p => p.IdEncuesta == idEncuesta)
                .ToListAsync();

            ViewBag.IdEncuesta = idEncuesta;
            ViewBag.IdEncuestado = idEncuestado;

            return View("TakeSurvey", preguntas);
        }


        public IActionResult SurveyCompleted()
        {
            return View();
        }

       
    }
}
