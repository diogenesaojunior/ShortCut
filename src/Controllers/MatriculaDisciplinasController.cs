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
    public class MatriculaDisciplinasController : Controller
    {
        private readonly MeuContexto _context;

        public MatriculaDisciplinasController(MeuContexto context)
        {
            _context = context;
        }

        // GET: MatriculaDisciplinas
        public async Task<IActionResult> Index()
        {
            var meuContexto = _context.MatriculaDisciplinas.Include(m => m.Aluno).Include(m => m.Disciplina);
            return View(await meuContexto.ToListAsync());
        }

        // GET: MatriculaDisciplinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matriculaDisciplina = await _context.MatriculaDisciplinas
                .Include(m => m.Aluno)
                .Include(m => m.Disciplina)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matriculaDisciplina == null)
            {
                return NotFound();
            }

            return View(matriculaDisciplina);
        }

        // GET: MatriculaDisciplinas/Create
        public IActionResult Create()
        {
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "CPF");
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao");
            return View();
        }

        // POST: MatriculaDisciplinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status,AlunoId,DisciplinaId")] MatriculaDisciplina matriculaDisciplina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matriculaDisciplina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "CPF", matriculaDisciplina.AlunoId);
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", matriculaDisciplina.DisciplinaId);
            return View(matriculaDisciplina);
        }

        // GET: MatriculaDisciplinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matriculaDisciplina = await _context.MatriculaDisciplinas.FindAsync(id);
            if (matriculaDisciplina == null)
            {
                return NotFound();
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "CPF", matriculaDisciplina.AlunoId);
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", matriculaDisciplina.DisciplinaId);
            return View(matriculaDisciplina);
        }

        // POST: MatriculaDisciplinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,AlunoId,DisciplinaId")] MatriculaDisciplina matriculaDisciplina)
        {
            if (id != matriculaDisciplina.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matriculaDisciplina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatriculaDisciplinaExists(matriculaDisciplina.Id))
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
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "CPF", matriculaDisciplina.AlunoId);
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", matriculaDisciplina.DisciplinaId);
            return View(matriculaDisciplina);
        }

        // GET: MatriculaDisciplinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matriculaDisciplina = await _context.MatriculaDisciplinas
                .Include(m => m.Aluno)
                .Include(m => m.Disciplina)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matriculaDisciplina == null)
            {
                return NotFound();
            }

            return View(matriculaDisciplina);
        }

        // POST: MatriculaDisciplinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matriculaDisciplina = await _context.MatriculaDisciplinas.FindAsync(id);
            _context.MatriculaDisciplinas.Remove(matriculaDisciplina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatriculaDisciplinaExists(int id)
        {
            return _context.MatriculaDisciplinas.Any(e => e.Id == id);
        }
    }
}
