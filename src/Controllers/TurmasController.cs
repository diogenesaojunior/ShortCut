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
    [Authorize(Roles = "Gerente")]
    public class TurmasController : Controller
    {
        private readonly MeuContexto _context;

        public TurmasController(MeuContexto context)
        {
            _context = context;
        }

        // GET: Turmas
        public async Task<IActionResult> Index()
        {
            var meuContexto = _context.Turmas.Include(t => t.DisciplinaOfertada).Include(t => t.Docente);
            return View(await meuContexto.ToListAsync());
        }

        // GET: Turmas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _context.Turmas
                .Include(t => t.DisciplinaOfertada)
                .Include(t => t.Docente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turma == null)
            {
                return NotFound();
            }

            return View(turma);
        }

        // GET: Turmas/Create
        public IActionResult Create()
        {
            ViewData["DisciplinaOfertadaId"] = new SelectList(_context.DisciplinaOfertadas, "Id", "Id");
            ViewData["DocenteId"] = new SelectList(_context.Docentes, "Id", "Nome");
            return View();
        }

        // POST: Turmas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DisciplinaOfertadaId,DocenteId")] Turma turma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisciplinaOfertadaId"] = new SelectList(_context.DisciplinaOfertadas, "Id", "Id", turma.DisciplinaOfertadaId);
            ViewData["DocenteId"] = new SelectList(_context.Docentes, "Id", "Nome", turma.DocenteId);
            return View(turma);
        }

        // GET: Turmas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _context.Turmas.FindAsync(id);
            if (turma == null)
            {
                return NotFound();
            }
            ViewData["DisciplinaOfertadaId"] = new SelectList(_context.DisciplinaOfertadas, "Id", "Id", turma.DisciplinaOfertadaId);
            ViewData["DocenteId"] = new SelectList(_context.Docentes, "Id", "Nome", turma.DocenteId);
            return View(turma);
        }

        // POST: Turmas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DisciplinaOfertadaId,DocenteId")] Turma turma)
        {
            if (id != turma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurmaExists(turma.Id))
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
            ViewData["DisciplinaOfertadaId"] = new SelectList(_context.DisciplinaOfertadas, "Id", "Id", turma.DisciplinaOfertadaId);
            ViewData["DocenteId"] = new SelectList(_context.Docentes, "Id", "Nome", turma.DocenteId);
            return View(turma);
        }

        // GET: Turmas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _context.Turmas
                .Include(t => t.DisciplinaOfertada)
                .Include(t => t.Docente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turma == null)
            {
                return NotFound();
            }

            return View(turma);
        }

        // POST: Turmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);
            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurmaExists(int id)
        {
            return _context.Turmas.Any(e => e.Id == id);
        }
    }
}
