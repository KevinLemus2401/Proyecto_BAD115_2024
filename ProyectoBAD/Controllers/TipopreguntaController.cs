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
    public class TipopreguntaController : Controller
    {
        private readonly sisencuestasContext _context;

        public TipopreguntaController(sisencuestasContext context)
        {
            _context = context;
        }

        // GET: Tipopregunta
        public async Task<IActionResult> Index()
        {
              return _context.Tipopregunta != null ? 
                          View(await _context.Tipopregunta.ToListAsync()) :
                          Problem("Entity set 'sisencuestasContext.Tipopregunta'  is null.");
        }

        // GET: Tipopregunta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tipopregunta == null)
            {
                return NotFound();
            }

            var tipopreguntum = await _context.Tipopregunta
                .FirstOrDefaultAsync(m => m.TipoPreguntaId == id);
            if (tipopreguntum == null)
            {
                return NotFound();
            }

            return View(tipopreguntum);
        }

        // GET: Tipopregunta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tipopregunta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoPreguntaId,NombreTipoPregunta,DescripcionTipoPregunta")] Tipopreguntum tipopreguntum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipopreguntum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipopreguntum);
        }

        // GET: Tipopregunta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tipopregunta == null)
            {
                return NotFound();
            }

            var tipopreguntum = await _context.Tipopregunta.FindAsync(id);
            if (tipopreguntum == null)
            {
                return NotFound();
            }
            return View(tipopreguntum);
        }

        // POST: Tipopregunta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoPreguntaId,NombreTipoPregunta,DescripcionTipoPregunta")] Tipopreguntum tipopreguntum)
        {
            if (id != tipopreguntum.TipoPreguntaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipopreguntum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipopreguntumExists(tipopreguntum.TipoPreguntaId))
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
            return View(tipopreguntum);
        }

        // GET: Tipopregunta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tipopregunta == null)
            {
                return NotFound();
            }

            var tipopreguntum = await _context.Tipopregunta
                .FirstOrDefaultAsync(m => m.TipoPreguntaId == id);
            if (tipopreguntum == null)
            {
                return NotFound();
            }

            return View(tipopreguntum);
        }

        // POST: Tipopregunta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tipopregunta == null)
            {
                return Problem("Entity set 'sisencuestasContext.Tipopregunta'  is null.");
            }
            var tipopreguntum = await _context.Tipopregunta.FindAsync(id);
            if (tipopreguntum != null)
            {
                _context.Tipopregunta.Remove(tipopreguntum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipopreguntumExists(int id)
        {
          return (_context.Tipopregunta?.Any(e => e.TipoPreguntaId == id)).GetValueOrDefault();
        }
    }
}
