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
    public class PeriodoCurricularesController : Controller
    {
        private readonly MeuContexto _context;

        public PeriodoCurricularesController(MeuContexto context)
        {
            _context = context;
        }

        // GET: PeriodoCurriculares
        public async Task<IActionResult> Index()
        {
            return View(await _context.PeriodoCurriculares.ToListAsync());
        }

        // GET: PeriodoCurriculares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periodoCurricular = await _context.PeriodoCurriculares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (periodoCurricular == null)
            {
                return NotFound();
            }

            return View(periodoCurricular);
        }

        // GET: PeriodoCurriculares/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PeriodoCurriculares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumOrdinal")] PeriodoCurricular periodoCurricular)
        {
            if (ModelState.IsValid)
            {
                _context.Add(periodoCurricular);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(periodoCurricular);
        }

        // GET: PeriodoCurriculares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periodoCurricular = await _context.PeriodoCurriculares.FindAsync(id);
            if (periodoCurricular == null)
            {
                return NotFound();
            }
            return View(periodoCurricular);
        }

        // POST: PeriodoCurriculares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumOrdinal")] PeriodoCurricular periodoCurricular)
        {
            if (id != periodoCurricular.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(periodoCurricular);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeriodoCurricularExists(periodoCurricular.Id))
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
            return View(periodoCurricular);
        }

        // GET: PeriodoCurriculares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periodoCurricular = await _context.PeriodoCurriculares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (periodoCurricular == null)
            {
                return NotFound();
            }

            return View(periodoCurricular);
        }

        // POST: PeriodoCurriculares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var periodoCurricular = await _context.PeriodoCurriculares.FindAsync(id);
            _context.PeriodoCurriculares.Remove(periodoCurricular);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeriodoCurricularExists(int id)
        {
            return _context.PeriodoCurriculares.Any(e => e.Id == id);
        }
    }
}
