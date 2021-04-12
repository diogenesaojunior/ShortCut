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
    public class HoraAulaController : Controller
    {
        private readonly MeuContexto _context;

        public HoraAulaController(MeuContexto context)
        {
            _context = context;
        }

        // GET: HoraAula
        public async Task<IActionResult> Index()
        {
            return View(await _context.HoraAulas.ToListAsync());
        }

        // GET: HoraAula/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horaAula = await _context.HoraAulas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horaAula == null)
            {
                return NotFound();
            }

            return View(horaAula);
        }

        // GET: HoraAula/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HoraAula/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,DtInicio,DtFim")] HoraAula horaAula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horaAula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(horaAula);
        }

        // GET: HoraAula/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horaAula = await _context.HoraAulas.FindAsync(id);
            if (horaAula == null)
            {
                return NotFound();
            }
            return View(horaAula);
        }

        // POST: HoraAula/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,DtInicio,DtFim")] HoraAula horaAula)
        {
            if (id != horaAula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horaAula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoraAulaExists(horaAula.Id))
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
            return View(horaAula);
        }

        // GET: HoraAula/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horaAula = await _context.HoraAulas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horaAula == null)
            {
                return NotFound();
            }

            return View(horaAula);
        }

        // POST: HoraAula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horaAula = await _context.HoraAulas.FindAsync(id);
            _context.HoraAulas.Remove(horaAula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoraAulaExists(int id)
        {
            return _context.HoraAulas.Any(e => e.Id == id);
        }
    }
}
