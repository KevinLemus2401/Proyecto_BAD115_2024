﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoBAD.Models;

namespace ProyectoBAD.Controllers
{
    public class EncuestaController : Controller
    {
        private readonly sisencuestasContext _context;

        public EncuestaController(sisencuestasContext context)
        {
            _context = context;
        }

        // GET: Encuesta
        public async Task<IActionResult> Index()
        {
              return _context.Encuesta != null ? 
                          View(await _context.Encuesta.ToListAsync()) :
                          Problem("Entity set 'sisencuestasContext.Encuesta'  is null.");
        }

        // GET: Encuesta/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Encuesta == null)
            {
                return NotFound();
            }

            var encuestum = await _context.Encuesta
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEncuesta,IdUsuario,TituloEncuesta,ObjetivoEncuesta,GrupometaEncuesta,IndicacionesEncuesta,FechaEncuesta,EstadoEncuesta")] Encuestum encuestum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(encuestum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(encuestum);
        }

        // GET: Encuesta/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Encuesta == null)
            {
                return NotFound();
            }

            var encuestum = await _context.Encuesta.FindAsync(id);
            if (encuestum == null)
            {
                return NotFound();
            }
            return View(encuestum);
        }

        // POST: Encuesta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdEncuesta,IdUsuario,TituloEncuesta,ObjetivoEncuesta,GrupometaEncuesta,IndicacionesEncuesta,FechaEncuesta,EstadoEncuesta")] Encuestum encuestum)
        {
            if (id != encuestum.IdEncuesta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(encuestum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncuestumExists(encuestum.IdEncuesta))
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
            return View(encuestum);
        }

        // GET: Encuesta/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Encuesta == null)
            {
                return NotFound();
            }

            var encuestum = await _context.Encuesta
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
        public async Task<IActionResult> DeleteConfirmed(decimal id)
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

        private bool EncuestumExists(decimal id)
        {
          return (_context.Encuesta?.Any(e => e.IdEncuesta == id)).GetValueOrDefault();
        }
    }
}