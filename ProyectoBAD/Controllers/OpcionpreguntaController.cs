using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoBAD.Models;

namespace ProyectoBAD.Controllers
{
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
              return _context.Opcionpregunta != null ? 
                          View(await _context.Opcionpregunta.ToListAsync()) :
                          Problem("Entity set 'sisencuestasContext.Opcionpregunta'  is null.");
        }

        // GET: Opcionpregunta/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Opcionpregunta == null)
            {
                return NotFound();
            }

            var opcionpreguntum = await _context.Opcionpregunta
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
            return View(opcionpreguntum);
        }

        // GET: Opcionpregunta/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
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
            return View(opcionpreguntum);
        }

        // POST: Opcionpregunta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("OpcionId,IdPregunta,ValorOpcion,DescripcionOpcion,EstadoOpcion,OrdenOpcion")] Opcionpreguntum opcionpreguntum)
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
            return View(opcionpreguntum);
        }

        // GET: Opcionpregunta/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Opcionpregunta == null)
            {
                return NotFound();
            }

            var opcionpreguntum = await _context.Opcionpregunta
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
        public async Task<IActionResult> DeleteConfirmed(decimal id)
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

        private bool OpcionpreguntumExists(decimal id)
        {
          return (_context.Opcionpregunta?.Any(e => e.OpcionId == id)).GetValueOrDefault();
        }
    }
}
