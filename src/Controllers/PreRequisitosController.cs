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
    public class PreRequisitosController : Controller
    {
        private readonly MeuContexto _context;

        public PreRequisitosController(MeuContexto context)
        {
            _context = context;
        }

        // GET: PreRequisitos
        public async Task<IActionResult> Index()
        {
            var meuContexto = _context.PreRequisitos.Include(p => p.Liberada).Include(p => p.Liberadora);
            return View(await meuContexto.ToListAsync());
        }

        // GET: PreRequisitos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preRequisito = await _context.PreRequisitos
                .Include(p => p.Liberada)
                .Include(p => p.Liberadora)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preRequisito == null)
            {
                return NotFound();
            }

            return View(preRequisito);
        }

        // GET: PreRequisitos/Create
        public IActionResult Create()
        {
            ViewData["LiberadaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao");
            ViewData["LiberadoraId"] = new SelectList(_context.Disciplinas, "Id", "Descricao");
            return View();
        }

        // POST: PreRequisitos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LiberadaId,LiberadoraId")] PreRequisito preRequisito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preRequisito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LiberadaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", preRequisito.LiberadaId);
            ViewData["LiberadoraId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", preRequisito.LiberadoraId);
            return View(preRequisito);
        }

        // GET: PreRequisitos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preRequisito = await _context.PreRequisitos.FindAsync(id);
            if (preRequisito == null)
            {
                return NotFound();
            }
            ViewData["LiberadaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", preRequisito.LiberadaId);
            ViewData["LiberadoraId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", preRequisito.LiberadoraId);
            return View(preRequisito);
        }

        // POST: PreRequisitos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LiberadaId,LiberadoraId")] PreRequisito preRequisito)
        {
            if (id != preRequisito.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preRequisito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreRequisitoExists(preRequisito.Id))
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
            ViewData["LiberadaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", preRequisito.LiberadaId);
            ViewData["LiberadoraId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", preRequisito.LiberadoraId);
            return View(preRequisito);
        }

        // GET: PreRequisitos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preRequisito = await _context.PreRequisitos
                .Include(p => p.Liberada)
                .Include(p => p.Liberadora)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preRequisito == null)
            {
                return NotFound();
            }

            return View(preRequisito);
        }

        // POST: PreRequisitos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var preRequisito = await _context.PreRequisitos.FindAsync(id);
            _context.PreRequisitos.Remove(preRequisito);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreRequisitoExists(int id)
        {
            return _context.PreRequisitos.Any(e => e.Id == id);
        }
    }
}
