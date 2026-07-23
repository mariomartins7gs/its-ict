using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeStores.Data;
using BikeStores.Models;

namespace BikeStores.Controllers;

public class CustomersController : Controller
{
    private readonly BikeStoresContext _context;

    public CustomersController(BikeStoresContext context)
    {
        _context = context;
    }

    // GET: /Customers
    // GET: /Customers?search=term
    public async Task<IActionResult> Index(string search)
    {
        ViewBag.Search = search ?? string.Empty;

        var customers = _context.Customers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            customers = customers.Where(c =>
                c.FirstName.Contains(search) ||
                c.LastName.Contains(search) ||
                (c.City != null && c.City.Contains(search)) ||
                (c.State != null && c.State.Contains(search)));
        }

        var result = await customers
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ToListAsync();

        return View(result);
    }

    // GET: /Customers/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var customer = await _context.Customers
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.CustomerId == id);

        if (customer == null)
            return NotFound();

        return View(customer);
    }
}
