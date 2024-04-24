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
    public class RespuestaController : Controller
    {
        private readonly sisencuestasContext _context;

        public RespuestaController(sisencuestasContext context)
        {
            _context = context;
        }

        // GET: Respuesta
        public async Task<IActionResult> Index()
        {
              return _context.Respuesta != null ? 
                          View(await _context.Respuesta.ToListAsync()) :
                          Problem("Entity set 'sisencuestasContext.Respuesta'  is null.");
        }

        // GET: Respuesta/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Respuesta == null)
            {
                return NotFound();
            }

            var respuestum = await _context.Respuesta
                .FirstOrDefaultAsync(m => m.RespuestaId == id);
            if (respuestum == null)
            {
                return NotFound();
            }

            return View(respuestum);
        }

        // GET: Respuesta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Respuesta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RespuestaId,OpcionId,IdPregunta,IdEncuestado,IdEncuesta,FechaRespuesta,TextoRespuesta")] Respuestum respuestum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(respuestum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(respuestum);
        }

        // GET: Respuesta/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Respuesta == null)
            {
                return NotFound();
            }

            var respuestum = await _context.Respuesta.FindAsync(id);
            if (respuestum == null)
            {
                return NotFound();
            }
            return View(respuestum);
        }

        // POST: Respuesta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("RespuestaId,OpcionId,IdPregunta,IdEncuestado,IdEncuesta,FechaRespuesta,TextoRespuesta")] Respuestum respuestum)
        {
            if (id != respuestum.RespuestaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(respuestum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RespuestumExists(respuestum.RespuestaId))
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
            return View(respuestum);
        }

        // GET: Respuesta/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Respuesta == null)
            {
                return NotFound();
            }

            var respuestum = await _context.Respuesta
                .FirstOrDefaultAsync(m => m.RespuestaId == id);
            if (respuestum == null)
            {
                return NotFound();
            }

            return View(respuestum);
        }

        // POST: Respuesta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Respuesta == null)
            {
                return Problem("Entity set 'sisencuestasContext.Respuesta'  is null.");
            }
            var respuestum = await _context.Respuesta.FindAsync(id);
            if (respuestum != null)
            {
                _context.Respuesta.Remove(respuestum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RespuestumExists(decimal id)
        {
          return (_context.Respuesta?.Any(e => e.RespuestaId == id)).GetValueOrDefault();
        }
    }
}
