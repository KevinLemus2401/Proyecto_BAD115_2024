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
    public class OpcionpreguntaController : Controller
    {
        private readonly sisencuestasContext _context;

        public OpcionpreguntaController(sisencuestasContext context)
        {
            _context = context;
        }

        // GET: Opcionpregunta
        public async Task<IActionResult> Index()
        {
            var sisencuestasContext = _context.Opcionpregunta.Include(o => o.IdPreguntaNavigation);
            return View(await sisencuestasContext.ToListAsync());
        }

        // GET: Opcionpregunta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Opcionpregunta == null)
            {
                return NotFound();
            }

            var opcionpreguntum = await _context.Opcionpregunta
                .Include(o => o.IdPreguntaNavigation)
                .FirstOrDefaultAsync(m => m.OpcionId == id);
            if (opcionpreguntum == null)
            {
                return NotFound();
            }

            return View(opcionpreguntum);
        }

        // GET: Opcionpregunta/Create
        public IActionResult Create()
        {
            ViewData["IdPregunta"] = new SelectList(_context.Pregunta, "IdPregunta", "IdPregunta");
            return View();
        }

        // POST: Opcionpregunta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OpcionId,IdPregunta,ValorOpcion,DescripcionOpcion,EstadoOpcion,OrdenOpcion")] Opcionpreguntum opcionpreguntum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opcionpreguntum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPregunta"] = new SelectList(_context.Pregunta, "IdPregunta", "IdPregunta", opcionpreguntum.IdPregunta);
            return View(opcionpreguntum);
        }

        // GET: Opcionpregunta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Opcionpregunta == null)
            {
                return NotFound();
            }

            var opcionpreguntum = await _context.Opcionpregunta.FindAsync(id);
            if (opcionpreguntum == null)
            {
                return NotFound();
            }
            ViewData["IdPregunta"] = new SelectList(_context.Pregunta, "IdPregunta", "IdPregunta", opcionpreguntum.IdPregunta);
            return View(opcionpreguntum);
        }

        // POST: Opcionpregunta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OpcionId,IdPregunta,ValorOpcion,DescripcionOpcion,EstadoOpcion,OrdenOpcion")] Opcionpreguntum opcionpreguntum)
        {
            if (id != opcionpreguntum.OpcionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opcionpreguntum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpcionpreguntumExists(opcionpreguntum.OpcionId))
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
            ViewData["IdPregunta"] = new SelectList(_context.Pregunta, "IdPregunta", "IdPregunta", opcionpreguntum.IdPregunta);
            return View(opcionpreguntum);
        }

        // GET: Opcionpregunta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Opcionpregunta == null)
            {
                return NotFound();
            }

            var opcionpreguntum = await _context.Opcionpregunta
                .Include(o => o.IdPreguntaNavigation)
                .FirstOrDefaultAsync(m => m.OpcionId == id);
            if (opcionpreguntum == null)
            {
                return NotFound();
            }

            return View(opcionpreguntum);
        }

        // POST: Opcionpregunta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Opcionpregunta == null)
            {
                return Problem("Entity set 'sisencuestasContext.Opcionpregunta'  is null.");
            }
            var opcionpreguntum = await _context.Opcionpregunta.FindAsync(id);
            if (opcionpreguntum != null)
            {
                _context.Opcionpregunta.Remove(opcionpreguntum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpcionpreguntumExists(int id)
        {
          return (_context.Opcionpregunta?.Any(e => e.OpcionId == id)).GetValueOrDefault();
        }
    }
}
