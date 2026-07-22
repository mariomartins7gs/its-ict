using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADO.NET.MVC_GAM.Models;

namespace ADO.NET.MVC_GAM.Controllers;

public class OpereController : Controller
{
    private readonly GAMContext _context;

    public OpereController(GAMContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var opere = _context.Opere.Include(o => o.IdAutoreNavigation);
        return View(await opere.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var opera = await _context.Opere
            .Include(o => o.IdAutoreNavigation)
            .Include(o => o.OperaMateriali)
                .ThenInclude(om => om.IdMaterialeNavigation)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (opera == null) return NotFound();

        return View(opera);
    }

    public IActionResult Create()
    {
        ViewData["IdAutore"] = new SelectList(_context.Autori, "Id", "Nominativo");
        ViewData["Materiali"] = new MultiSelectList(_context.Materiali, "Id", "Denominazione");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Inventario,IdAutore,Ambito_culturale,Datazione,Titolo_soggetto,Immagine,lsreferenceby")] Opera opera, int[]? materialiSelezionati)
    {
        if (ModelState.IsValid)
        {
            _context.Add(opera);
            await _context.SaveChangesAsync();

            if (materialiSelezionati != null)
            {
                foreach (var idMateriale in materialiSelezionati)
                {
                    _context.OperaMateriali.Add(new OperaMateriale
                    {
                        IdOpera = opera.Id,
                        IdMateriale = idMateriale
                    });
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        ViewData["IdAutore"] = new SelectList(_context.Autori, "Id", "Nominativo", opera.IdAutore);
        return View(opera);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var opera = await _context.Opere
            .Include(o => o.OperaMateriali)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (opera == null) return NotFound();

        ViewData["IdAutore"] = new SelectList(_context.Autori, "Id", "Nominativo", opera.IdAutore);
        ViewData["Materiali"] = new MultiSelectList(_context.Materiali, "Id", "Denominazione", opera.OperaMateriali.Select(om => om.IdMateriale));
        return View(opera);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Inventario,IdAutore,Ambito_culturale,Datazione,Titolo_soggetto,Immagine,lsreferenceby")] Opera opera, int[]? materialiSelezionati)
    {
        if (id != opera.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(opera);

                var materialiEsistenti = _context.OperaMateriali.Where(om => om.IdOpera == id);
                _context.OperaMateriali.RemoveRange(materialiEsistenti);

                if (materialiSelezionati != null)
                {
                    foreach (var idMateriale in materialiSelezionati)
                    {
                        _context.OperaMateriali.Add(new OperaMateriale
                        {
                            IdOpera = id,
                            IdMateriale = idMateriale
                        });
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Opere.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["IdAutore"] = new SelectList(_context.Autori, "Id", "Nominativo", opera.IdAutore);
        return View(opera);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var opera = await _context.Opere
            .Include(o => o.IdAutoreNavigation)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (opera == null) return NotFound();

        return View(opera);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var opera = await _context.Opere.FindAsync(id);
        if (opera != null)
        {
            _context.Opere.Remove(opera);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
