using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeStores.Data;
using BikeStores.Models;

namespace BikeStores.Controllers;

public class StoresController : Controller
{
    private readonly BikeStoresContext _context;

    public StoresController(BikeStoresContext context)
    {
        _context = context;
    }

    // GET: /Stores
    // GET: /Stores?search=term
    public async Task<IActionResult> Index(string search)
    {
        ViewBag.Search = search ?? string.Empty;

        var stores = _context.Stores.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            stores = stores.Where(s =>
                (s.City != null && s.City.Contains(search)) ||
                (s.State != null && s.State.Contains(search)));
        }

        var result = await stores
            .OrderBy(s => s.State)
            .ThenBy(s => s.City)
            .ToListAsync();

        return View(result);
    }

    // GET: /Stores/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var store = await _context.Stores
            .Include(s => s.Staff)
            .Include(s => s.Stocks)
                .ThenInclude(st => st.Product)
            .FirstOrDefaultAsync(s => s.StoreId == id);

        if (store == null)
            return NotFound();

        return View(store);
    }
}
