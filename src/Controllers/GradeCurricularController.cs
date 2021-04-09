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
    public class GradeCurricularController : Controller
    {
        private readonly MeuContexto _context;

        public GradeCurricularController(MeuContexto context)
        {
            _context = context;
        }

        // GET: GradeCurricular
        public async Task<IActionResult> Index()
        {
            var meuContexto = _context.GradeCurriculars.Include(g => g.Curso).Include(g => g.Disciplina).Include(g => g.PeriodoCurricular);
            return View(await meuContexto.ToListAsync());
        }

        // GET: GradeCurricular/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeCurricular = await _context.GradeCurriculars
                .Include(g => g.Curso)
                .Include(g => g.Disciplina)
                .Include(g => g.PeriodoCurricular)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gradeCurricular == null)
            {
                return NotFound();
            }

            return View(gradeCurricular);
        }

        // GET: GradeCurricular/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Descricao");
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao");
            ViewData["PeriodoCurricularId"] = new SelectList(_context.PeriodoCurriculares, "Id", "Id");
            return View();
        }

        // POST: GradeCurricular/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,CursoId,PeriodoCurricularId,DisciplinaId")] GradeCurricular gradeCurricular)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gradeCurricular);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Descricao", gradeCurricular.CursoId);
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", gradeCurricular.DisciplinaId);
            ViewData["PeriodoCurricularId"] = new SelectList(_context.PeriodoCurriculares, "Id", "Id", gradeCurricular.PeriodoCurricularId);
            return View(gradeCurricular);
        }

        // GET: GradeCurricular/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeCurricular = await _context.GradeCurriculars.FindAsync(id);
            if (gradeCurricular == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Descricao", gradeCurricular.CursoId);
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", gradeCurricular.DisciplinaId);
            ViewData["PeriodoCurricularId"] = new SelectList(_context.PeriodoCurriculares, "Id", "Id", gradeCurricular.PeriodoCurricularId);
            return View(gradeCurricular);
        }

        // POST: GradeCurricular/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,CursoId,PeriodoCurricularId,DisciplinaId")] GradeCurricular gradeCurricular)
        {
            if (id != gradeCurricular.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gradeCurricular);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeCurricularExists(gradeCurricular.Id))
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
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Descricao", gradeCurricular.CursoId);
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", gradeCurricular.DisciplinaId);
            ViewData["PeriodoCurricularId"] = new SelectList(_context.PeriodoCurriculares, "Id", "Id", gradeCurricular.PeriodoCurricularId);
            return View(gradeCurricular);
        }

        // GET: GradeCurricular/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeCurricular = await _context.GradeCurriculars
                .Include(g => g.Curso)
                .Include(g => g.Disciplina)
                .Include(g => g.PeriodoCurricular)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gradeCurricular == null)
            {
                return NotFound();
            }

            return View(gradeCurricular);
        }

        // POST: GradeCurricular/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gradeCurricular = await _context.GradeCurriculars.FindAsync(id);
            _context.GradeCurriculars.Remove(gradeCurricular);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeCurricularExists(int id)
        {
            return _context.GradeCurriculars.Any(e => e.Id == id);
        }
    }
}
