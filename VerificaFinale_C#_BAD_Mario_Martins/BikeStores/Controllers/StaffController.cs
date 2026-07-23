using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeStores.Data;
using BikeStores.Models;

namespace BikeStores.Controllers;

public class StaffController : Controller
{
    private readonly BikeStoresContext _context;

    public StaffController(BikeStoresContext context)
    {
        _context = context;
    }

    // GET: /Staff
    // GET: /Staff?search=term
    public async Task<IActionResult> Index(string search)
    {
        ViewBag.Search = search ?? string.Empty;

        var staff = _context.Staffs
            .Include(s => s.Store)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            staff = staff.Where(s =>
                s.FirstName.Contains(search) ||
                s.LastName.Contains(search));
        }

        var result = await staff
            .OrderBy(s => s.LastName)
            .ThenBy(s => s.FirstName)
            .ToListAsync();

        return View(result);
    }

    // GET: /Staff/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var staff = await _context.Staffs
            .Include(s => s.Store)
            .Include(s => s.Manager)
            .Include(s => s.InverseManager)
            .FirstOrDefaultAsync(s => s.StaffId == id);

        if (staff == null)
            return NotFound();

        return View(staff);
    }
}
