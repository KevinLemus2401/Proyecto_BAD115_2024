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
    public class EncuestadosController : Controller
    {
        private readonly sisencuestasContext _context;

        public EncuestadosController(sisencuestasContext context)
        {
            _context = context;
        }

        // GET: Encuestados
        public async Task<IActionResult> Index()
        {
              return _context.Encuestados != null ? 
                          View(await _context.Encuestados.ToListAsync()) :
                          Problem("Entity set 'sisencuestasContext.Encuestados'  is null.");
        }

        // GET: Encuestados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Encuestados == null)
            {
                return NotFound();
            }

            var encuestado = await _context.Encuestados
                .FirstOrDefaultAsync(m => m.IdEncuestado == id);
            if (encuestado == null)
            {
                return NotFound();
            }

            return View(encuestado);
        }

        // GET: Encuestados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Encuestados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEncuestado,EmailEncuestado,FechaNacEncuesta,GenEncuestado")] Encuestado encuestado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(encuestado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(encuestado);
        }

        // GET: Encuestados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Encuestados == null)
            {
                return NotFound();
            }

            var encuestado = await _context.Encuestados.FindAsync(id);
            if (encuestado == null)
            {
                return NotFound();
            }
            return View(encuestado);
        }

        // POST: Encuestados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEncuestado,EmailEncuestado,FechaNacEncuesta,GenEncuestado")] Encuestado encuestado)
        {
            if (id != encuestado.IdEncuestado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(encuestado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncuestadoExists(encuestado.IdEncuestado))
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
            return View(encuestado);
        }

        // GET: Encuestados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Encuestados == null)
            {
                return NotFound();
            }

            var encuestado = await _context.Encuestados
                .FirstOrDefaultAsync(m => m.IdEncuestado == id);
            if (encuestado == null)
            {
                return NotFound();
            }

            return View(encuestado);
        }

        // POST: Encuestados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Encuestados == null)
            {
                return Problem("Entity set 'sisencuestasContext.Encuestados'  is null.");
            }
            var encuestado = await _context.Encuestados.FindAsync(id);
            if (encuestado != null)
            {
                _context.Encuestados.Remove(encuestado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncuestadoExists(int id)
        {
          return (_context.Encuestados?.Any(e => e.IdEncuestado == id)).GetValueOrDefault();
        }
    }
}
