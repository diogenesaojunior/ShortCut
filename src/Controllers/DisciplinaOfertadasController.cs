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
    public class DisciplinaOfertadasController : Controller
    {
        private readonly MeuContexto _context;

        public DisciplinaOfertadasController(MeuContexto context)
        {
            _context = context;
        }

        // GET: DisciplinaOfertadas
        public async Task<IActionResult> Index()
        {
            var meuContexto = _context.DisciplinaOfertadas.Include(d => d.Disciplina).Include(d => d.PeriodoLetivo);
            return View(await meuContexto.ToListAsync());
        }

        // GET: DisciplinaOfertadas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinaOfertada = await _context.DisciplinaOfertadas
                .Include(d => d.Disciplina)
                .Include(d => d.PeriodoLetivo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disciplinaOfertada == null)
            {
                return NotFound();
            }

            return View(disciplinaOfertada);
        }

        // GET: DisciplinaOfertadas/Create
        public IActionResult Create()
        {
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao");
            ViewData["PeriodoLetivoId"] = new SelectList(_context.PeriodoLetivos, "Id", "Descricao");
            return View();
        }

        // POST: DisciplinaOfertadas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DisciplinaId,PeriodoLetivoId")] DisciplinaOfertada disciplinaOfertada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplinaOfertada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", disciplinaOfertada.DisciplinaId);
            ViewData["PeriodoLetivoId"] = new SelectList(_context.PeriodoLetivos, "Id", "Descricao", disciplinaOfertada.PeriodoLetivoId);
            return View(disciplinaOfertada);
        }

        // GET: DisciplinaOfertadas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinaOfertada = await _context.DisciplinaOfertadas.FindAsync(id);
            if (disciplinaOfertada == null)
            {
                return NotFound();
            }
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", disciplinaOfertada.DisciplinaId);
            ViewData["PeriodoLetivoId"] = new SelectList(_context.PeriodoLetivos, "Id", "Descricao", disciplinaOfertada.PeriodoLetivoId);
            return View(disciplinaOfertada);
        }

        // POST: DisciplinaOfertadas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DisciplinaId,PeriodoLetivoId")] DisciplinaOfertada disciplinaOfertada)
        {
            if (id != disciplinaOfertada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disciplinaOfertada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplinaOfertadaExists(disciplinaOfertada.Id))
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
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", disciplinaOfertada.DisciplinaId);
            ViewData["PeriodoLetivoId"] = new SelectList(_context.PeriodoLetivos, "Id", "Descricao", disciplinaOfertada.PeriodoLetivoId);
            return View(disciplinaOfertada);
        }

        // GET: DisciplinaOfertadas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinaOfertada = await _context.DisciplinaOfertadas
                .Include(d => d.Disciplina)
                .Include(d => d.PeriodoLetivo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disciplinaOfertada == null)
            {
                return NotFound();
            }

            return View(disciplinaOfertada);
        }

        // POST: DisciplinaOfertadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplinaOfertada = await _context.DisciplinaOfertadas.FindAsync(id);
            _context.DisciplinaOfertadas.Remove(disciplinaOfertada);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplinaOfertadaExists(int id)
        {
            return _context.DisciplinaOfertadas.Any(e => e.Id == id);
        }
    }
}
