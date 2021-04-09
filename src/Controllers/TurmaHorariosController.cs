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
    public class TurmaHorariosController : Controller
    {
        private readonly MeuContexto _context;

        public TurmaHorariosController(MeuContexto context)
        {
            _context = context;
        }

        // GET: TurmaHorarios
        public async Task<IActionResult> Index()
        {
            var meuContexto = _context.TurmaHorarios.Include(t => t.Horario).Include(t => t.Turma);
            return View(await meuContexto.ToListAsync());
        }

        // GET: TurmaHorarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turmaHorario = await _context.TurmaHorarios
                .Include(t => t.Horario)
                .Include(t => t.Turma)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turmaHorario == null)
            {
                return NotFound();
            }

            return View(turmaHorario);
        }

        // GET: TurmaHorarios/Create
        public IActionResult Create()
        {
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id");
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "Id", "Id");
            return View();
        }

        // POST: TurmaHorarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HorarioId,TurmaId")] TurmaHorario turmaHorario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turmaHorario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id", turmaHorario.HorarioId);
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "Id", "Id", turmaHorario.TurmaId);
            return View(turmaHorario);
        }

        // GET: TurmaHorarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turmaHorario = await _context.TurmaHorarios.FindAsync(id);
            if (turmaHorario == null)
            {
                return NotFound();
            }
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id", turmaHorario.HorarioId);
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "Id", "Id", turmaHorario.TurmaId);
            return View(turmaHorario);
        }

        // POST: TurmaHorarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HorarioId,TurmaId")] TurmaHorario turmaHorario)
        {
            if (id != turmaHorario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turmaHorario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurmaHorarioExists(turmaHorario.Id))
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
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id", turmaHorario.HorarioId);
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "Id", "Id", turmaHorario.TurmaId);
            return View(turmaHorario);
        }

        // GET: TurmaHorarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turmaHorario = await _context.TurmaHorarios
                .Include(t => t.Horario)
                .Include(t => t.Turma)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turmaHorario == null)
            {
                return NotFound();
            }

            return View(turmaHorario);
        }

        // POST: TurmaHorarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turmaHorario = await _context.TurmaHorarios.FindAsync(id);
            _context.TurmaHorarios.Remove(turmaHorario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurmaHorarioExists(int id)
        {
            return _context.TurmaHorarios.Any(e => e.Id == id);
        }
    }
}
