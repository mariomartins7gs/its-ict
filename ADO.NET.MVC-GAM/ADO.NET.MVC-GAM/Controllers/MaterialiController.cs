using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ADO.NET.MVC_GAM.Models;

namespace ADO.NET.MVC_GAM.Controllers;

public class MaterialiController : Controller
{
    private readonly GAMContext _context;

    public MaterialiController(GAMContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Materiali.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var materiale = await _context.Materiali
            .Include(m => m.OperaMateriali)
                .ThenInclude(om => om.IdOperaNavigation)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (materiale == null) return NotFound();

        return View(materiale);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Denominazione")] Materiale materiale)
    {
        if (ModelState.IsValid)
        {
            _context.Add(materiale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(materiale);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var materiale = await _context.Materiali.FindAsync(id);
        if (materiale == null) return NotFound();

        return View(materiale);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Denominazione")] Materiale materiale)
    {
        if (id != materiale.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(materiale);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Materiali.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(materiale);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var materiale = await _context.Materiali.FirstOrDefaultAsync(m => m.Id == id);
        if (materiale == null) return NotFound();

        return View(materiale);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var materiale = await _context.Materiali.FindAsync(id);
        if (materiale != null)
        {
            _context.Materiali.Remove(materiale);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "Impossibile eliminare questo materiale perché è associato a opere.");
                return View(materiale);
            }
        }

        return RedirectToAction(nameof(Index));
    }
}
