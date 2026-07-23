using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeStores.Data;
using BikeStores.Models;

namespace BikeStores.Controllers;

public class ProductsController : Controller
{
    private readonly BikeStoresContext _context;

    public ProductsController(BikeStoresContext context)
    {
        _context = context;
    }

    // GET: /Products
    // GET: /Products?search=term
    public async Task<IActionResult> Index(string search)
    {
        ViewBag.Search = search ?? string.Empty;

        var products = _context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            products = products.Where(p =>
                p.ProductName.Contains(search) ||
                p.Brand.BrandName.Contains(search) ||
                p.Category.CategoryName.Contains(search));
        }

        var result = await products
            .OrderBy(p => p.ProductName)
            .ToListAsync();

        return View(result);
    }

    // GET: /Products/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var product = await _context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Stocks)
                .ThenInclude(s => s.Store)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null)
            return NotFound();

        return View(product);
    }
}
