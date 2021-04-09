using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SisGerenciador.src.Data;
using SisGerenciador.src.Models;

namespace SisGerenciador.Views
{
    [Authorize(Roles = "Admin")]
    public class PeriodoLetivosController : Controller
    {
        private readonly MeuContexto _context;

        public PeriodoLetivosController(MeuContexto context)
        {
            _context = context;
        }

        // GET: PeriodoLetivos
        public async Task<IActionResult> Index()
        {
            return View(await _context.PeriodoLetivos.ToListAsync());
        }

        // GET: PeriodoLetivos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periodoLetivo = await _context.PeriodoLetivos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (periodoLetivo == null)
            {
                return NotFound();
            }

            return View(periodoLetivo);
        }

        // GET: PeriodoLetivos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PeriodoLetivos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,DtInicio,DtFim")] PeriodoLetivo periodoLetivo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(periodoLetivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(periodoLetivo);
        }

        // GET: PeriodoLetivos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periodoLetivo = await _context.PeriodoLetivos.FindAsync(id);
            if (periodoLetivo == null)
            {
                return NotFound();
            }
            return View(periodoLetivo);
        }

        // POST: PeriodoLetivos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,DtInicio,DtFim")] PeriodoLetivo periodoLetivo)
        {
            if (id != periodoLetivo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(periodoLetivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeriodoLetivoExists(periodoLetivo.Id))
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
            return View(periodoLetivo);
        }

        // GET: PeriodoLetivos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periodoLetivo = await _context.PeriodoLetivos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (periodoLetivo == null)
            {
                return NotFound();
            }

            return View(periodoLetivo);
        }

        // POST: PeriodoLetivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var periodoLetivo = await _context.PeriodoLetivos.FindAsync(id);
            _context.PeriodoLetivos.Remove(periodoLetivo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeriodoLetivoExists(int id)
        {
            return _context.PeriodoLetivos.Any(e => e.Id == id);
        }
    }
}
