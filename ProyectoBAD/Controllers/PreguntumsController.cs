using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoBAD.Models;

namespace ProyectoBAD.Controllers
{
    [Authorize]
    public class PreguntumsController : Controller
    {
        private readonly sisencuestasContext _context;

        public PreguntumsController(sisencuestasContext context)
        {
            _context = context;
        }

        // GET: Preguntums
        public async Task<IActionResult> Index()
        {
            var sisencuestasContext = _context.Pregunta.Include(p => p.IdEncuestaNavigation).Include(p => p.TipoPregunta);
            return View(await sisencuestasContext.ToListAsync());
        }

        // GET: Preguntums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pregunta == null)
            {
                return NotFound();
            }

            var preguntum = await _context.Pregunta
                .Include(p => p.IdEncuestaNavigation)
                .Include(p => p.TipoPregunta)
                .FirstOrDefaultAsync(m => m.IdPregunta == id);
            if (preguntum == null)
            {
                return NotFound();
            }

            return View(preguntum);
        }

        // GET: Preguntums/Create
        public IActionResult Create()
        {
            ViewData["IdEncuesta"] = new SelectList(_context.Encuesta, "IdEncuesta", "IdEncuesta");
            ViewData["TipoPreguntaId"] = new SelectList(_context.Tipopregunta, "TipoPreguntaId", "TipoPreguntaId");
            return View();
        }

        // POST: Preguntums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPregunta,IdEncuesta,TipoPreguntaId,DescripcionPregunta,RequeridaPregunta,OrdenPregunta")] Preguntum preguntum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preguntum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEncuesta"] = new SelectList(_context.Encuesta, "IdEncuesta", "IdEncuesta", preguntum.IdEncuesta);
            ViewData["TipoPreguntaId"] = new SelectList(_context.Tipopregunta, "TipoPreguntaId", "TipoPreguntaId", preguntum.TipoPreguntaId);
            return View(preguntum);
        }

        // GET: Preguntums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pregunta == null)
            {
                return NotFound();
            }

            var preguntum = await _context.Pregunta.FindAsync(id);
            if (preguntum == null)
            {
                return NotFound();
            }
            ViewData["IdEncuesta"] = new SelectList(_context.Encuesta, "IdEncuesta", "IdEncuesta", preguntum.IdEncuesta);
            ViewData["TipoPreguntaId"] = new SelectList(_context.Tipopregunta, "TipoPreguntaId", "TipoPreguntaId", preguntum.TipoPreguntaId);
            return View(preguntum);
        }

        // POST: Preguntums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPregunta,IdEncuesta,TipoPreguntaId,DescripcionPregunta,RequeridaPregunta,OrdenPregunta")] Preguntum preguntum)
        {
            if (id != preguntum.IdPregunta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preguntum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreguntumExists(preguntum.IdPregunta))
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
            ViewData["IdEncuesta"] = new SelectList(_context.Encuesta, "IdEncuesta", "IdEncuesta", preguntum.IdEncuesta);
            ViewData["TipoPreguntaId"] = new SelectList(_context.Tipopregunta, "TipoPreguntaId", "TipoPreguntaId", preguntum.TipoPreguntaId);
            return View(preguntum);
        }

        // GET: Preguntums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pregunta == null)
            {
                return NotFound();
            }

            var preguntum = await _context.Pregunta
                .Include(p => p.IdEncuestaNavigation)
                .Include(p => p.TipoPregunta)
                .FirstOrDefaultAsync(m => m.IdPregunta == id);
            if (preguntum == null)
            {
                return NotFound();
            }

            return View(preguntum);
        }

        // POST: Preguntums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pregunta == null)
            {
                return Problem("Entity set 'sisencuestasContext.Pregunta'  is null.");
            }
            var preguntum = await _context.Pregunta.FindAsync(id);
            if (preguntum != null)
            {
                _context.Pregunta.Remove(preguntum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreguntumExists(int id)
        {
          return (_context.Pregunta?.Any(e => e.IdPregunta == id)).GetValueOrDefault();
        }

        // GET: Pregunta/ShowOptions/5
        public async Task<IActionResult> ShowOptions(int? id)
        {
            if (id == null || _context.Pregunta == null)
            {
                return NotFound();
            }

            var preguntum = await _context.Pregunta
                .Include(e => e.Opcionpregunta)  // Incluir las Opciones
                .FirstOrDefaultAsync(m => m.IdPregunta == id);

            if (preguntum == null)
            {
                return NotFound();
            }

            ViewBag.PreguntaTitulo = preguntum.DescripcionPregunta;

            // Pasar los datos de las Opciones a la vista de preguntas existente
            return View("~/Views/Opcionpregunta/Index.cshtml", preguntum.Opcionpregunta);
        }
    }
}
