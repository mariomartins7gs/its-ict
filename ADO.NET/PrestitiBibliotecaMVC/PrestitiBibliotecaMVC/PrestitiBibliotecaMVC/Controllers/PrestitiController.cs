using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrestitiBibliotecaMVC.Models;

namespace PrestitiBibliotecaMVC.Controllers
{
    public class PrestitiController : Controller
    {
        private readonly PrestitiBibliotecaContext _context;

        public PrestitiController(PrestitiBibliotecaContext context)
        {
            _context = context;
        }

        // GET: Prestiti
        public async Task<IActionResult> Index()
        {
            var prestitiBibliotecaContext = _context.Prestiti.Include(p => p.IdLibroNavigation).Include(p => p.MatricolaNavigation);
            return View(await prestitiBibliotecaContext.ToListAsync());
        }

        // GET: Prestiti/Details/5
        public async Task<IActionResult> Details(int? idLibro, int? matricola)
        {
            if (idLibro == null || matricola == null)
            {
                return NotFound();
            }

            var prestito = await _context.Prestiti
                .Include(p => p.IdLibroNavigation)
                .Include(p => p.MatricolaNavigation)
                .FirstOrDefaultAsync(m => m.IdLibro == idLibro && m.Matricola == matricola);
            if (prestito == null)
            {
                return NotFound();
            }

            return View(prestito);
        }

        // GET: Prestiti/Create
        public IActionResult Create()
        {
            ViewData["IdLibro"] = new SelectList(_context.Libri, "Codice", "Titolo");
            ViewData["Matricola"] = new SelectList(_context.Studenti, "Matricola", "Cognome");
            return View();
        }

        // POST: Prestiti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLibro,Matricola,DataPrestito,DataRestituzione")] Prestito prestito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prestito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLibro"] = new SelectList(_context.Libri, "Codice", "Titolo", prestito.IdLibro);
            ViewData["Matricola"] = new SelectList(_context.Studenti, "Matricola", "Cognome", prestito.Matricola);
            return View(prestito);
        }

        // GET: Prestiti/Edit/5
        public async Task<IActionResult> Edit(int? idLibro, int? matricola)
        {
            if (idLibro == null || matricola == null)
            {
                return NotFound();
            }

            var prestito = await _context.Prestiti.FindAsync(idLibro, matricola);
            if (prestito == null)
            {
                return NotFound();
            }
            ViewData["IdLibro"] = new SelectList(_context.Libri, "Codice", "Titolo", prestito.IdLibro);
            ViewData["Matricola"] = new SelectList(_context.Studenti, "Matricola", "Cognome", prestito.Matricola);
            return View(prestito);
        }

        // POST: Prestiti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idLibro, int matricola, [Bind("IdLibro,Matricola,DataPrestito,DataRestituzione")] Prestito prestito)
        {
            if (idLibro != prestito.IdLibro || matricola != prestito.Matricola)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestitoExists(prestito.IdLibro, prestito.Matricola))
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
            ViewData["IdLibro"] = new SelectList(_context.Libri, "Codice", "Titolo", prestito.IdLibro);
            ViewData["Matricola"] = new SelectList(_context.Studenti, "Matricola", "Cognome", prestito.Matricola);
            return View(prestito);
        }

        // GET: Prestiti/Delete/5
        public async Task<IActionResult> Delete(int? idLibro, int? matricola)
        {
            if (idLibro == null || matricola == null)
            {
                return NotFound();
            }

            var prestito = await _context.Prestiti
                .Include(p => p.IdLibroNavigation)
                .Include(p => p.MatricolaNavigation)
                .FirstOrDefaultAsync(m => m.IdLibro == idLibro && m.Matricola == matricola);
            if (prestito == null)
            {
                return NotFound();
            }

            return View(prestito);
        }

        // POST: Prestiti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idLibro, int matricola)
        {
            var prestito = await _context.Prestiti.FindAsync(idLibro, matricola);
            if (prestito != null)
            {
                _context.Prestiti.Remove(prestito);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestitoExists(int idLibro, int matricola)
        {
            return _context.Prestiti.Any(e => e.IdLibro == idLibro && e.Matricola == matricola);
        }
    }
}
