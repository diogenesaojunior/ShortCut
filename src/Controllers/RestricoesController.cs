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
    public class RestricoesController : Controller
    {
        private readonly MeuContexto _context;

        public RestricoesController(MeuContexto context)
        {
            _context = context;
        }

        // GET: Restricoes
        public async Task<IActionResult> Index()
        {
            var meuContexto = _context.Restricoes.Include(r => r.Aluno).Include(r => r.Horario);
            return View(await meuContexto.ToListAsync());
        }

        // GET: Restricoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restricao = await _context.Restricoes
                .Include(r => r.Aluno)
                .Include(r => r.Horario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restricao == null)
            {
                return NotFound();
            }

            return View(restricao);
        }

        // GET: Restricoes/Create
        public IActionResult Create()
        {
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "CPF");
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id");
            return View();
        }

        // POST: Restricoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AlunoId,HorarioId")] Restricao restricao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restricao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "CPF", restricao.AlunoId);
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id", restricao.HorarioId);
            return View(restricao);
        }

        // GET: Restricoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restricao = await _context.Restricoes.FindAsync(id);
            if (restricao == null)
            {
                return NotFound();
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "CPF", restricao.AlunoId);
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id", restricao.HorarioId);
            return View(restricao);
        }

        // POST: Restricoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AlunoId,HorarioId")] Restricao restricao)
        {
            if (id != restricao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restricao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestricaoExists(restricao.Id))
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
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "CPF", restricao.AlunoId);
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id", restricao.HorarioId);
            return View(restricao);
        }

        // GET: Restricoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restricao = await _context.Restricoes
                .Include(r => r.Aluno)
                .Include(r => r.Horario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restricao == null)
            {
                return NotFound();
            }

            return View(restricao);
        }

        // POST: Restricoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restricao = await _context.Restricoes.FindAsync(id);
            _context.Restricoes.Remove(restricao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestricaoExists(int id)
        {
            return _context.Restricoes.Any(e => e.Id == id);
        }
    }
}
