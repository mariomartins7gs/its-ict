using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ADO.NET.MVC_GAM.Models;

namespace ADO.NET.MVC_GAM.Controllers;

public class AutoriController : Controller
{
    private readonly GAMContext _context;

    public AutoriController(GAMContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Autori.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var autore = await _context.Autori
            .Include(a => a.Opere)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (autore == null) return NotFound();

        return View(autore);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nominativo")] Autore autore)
    {
        if (ModelState.IsValid)
        {
            _context.Add(autore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(autore);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var autore = await _context.Autori.FindAsync(id);
        if (autore == null) return NotFound();

        return View(autore);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nominativo")] Autore autore)
    {
        if (id != autore.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(autore);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Autori.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(autore);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var autore = await _context.Autori.FirstOrDefaultAsync(m => m.Id == id);
        if (autore == null) return NotFound();

        return View(autore);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var autore = await _context.Autori.FindAsync(id);
        if (autore != null)
        {
            _context.Autori.Remove(autore);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "Impossibile eliminare questo autore perché ha opere associate.");
                return View(autore);
            }
        }

        return RedirectToAction(nameof(Index));
    }
}
