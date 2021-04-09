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
    public class DocentesController : Controller
    {
        private readonly MeuContexto _context;

        public DocentesController(MeuContexto context)
        {
            _context = context;
        }

        // GET: Docentes
        public async Task<IActionResult> Index()
        {
            var meuContexto = _context.Docentes.Include(d => d.Instituicao);
            return View(await meuContexto.ToListAsync());
        }

        // GET: Docentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docente = await _context.Docentes
                .Include(d => d.Instituicao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (docente == null)
            {
                return NotFound();
            }

            return View(docente);
        }

        // GET: Docentes/Create
        public IActionResult Create()
        {
            ViewData["InstituicaoId"] = new SelectList(_context.Instituicoes, "Id", "Descricao");
            return View();
        }

        // POST: Docentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CPF,Nome,Email,Endereco,Matricula,DtMatricula,InstituicaoId")] Docente docente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(docente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstituicaoId"] = new SelectList(_context.Instituicoes, "Id", "Descricao", docente.InstituicaoId);
            return View(docente);
        }

        // GET: Docentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docente = await _context.Docentes.FindAsync(id);
            if (docente == null)
            {
                return NotFound();
            }
            ViewData["InstituicaoId"] = new SelectList(_context.Instituicoes, "Id", "Descricao", docente.InstituicaoId);
            return View(docente);
        }

        // POST: Docentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CPF,Nome,Email,Endereco,Matricula,DtMatricula,InstituicaoId")] Docente docente)
        {
            if (id != docente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(docente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocenteExists(docente.Id))
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
            ViewData["InstituicaoId"] = new SelectList(_context.Instituicoes, "Id", "Descricao", docente.InstituicaoId);
            return View(docente);
        }

        // GET: Docentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docente = await _context.Docentes
                .Include(d => d.Instituicao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (docente == null)
            {
                return NotFound();
            }

            return View(docente);
        }

        // POST: Docentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var docente = await _context.Docentes.FindAsync(id);
            _context.Docentes.Remove(docente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocenteExists(int id)
        {
            return _context.Docentes.Any(e => e.Id == id);
        }
    }
}
