using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeStores.Data;
using BikeStores.Models;

namespace BikeStores.Controllers;

public class OrdersController : Controller
{
    private readonly BikeStoresContext _context;

    public OrdersController(BikeStoresContext context)
    {
        _context = context;
    }

    // GET: /Orders
    // GET: /Orders?search=term
    public async Task<IActionResult> Index(string search)
    {
        ViewBag.Search = search ?? string.Empty;

        var orders = _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Store)
            .Include(o => o.Staff)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            var searchLower = search.ToLower();

            // Try to match the search term to an order status (by name or number)
            byte? statusByte = null;
            if (byte.TryParse(search, out byte num) && num >= 1 && num <= 4)
                statusByte = num;
            else if (searchLower.Contains("pending")) statusByte = 1;
            else if (searchLower.Contains("processing")) statusByte = 2;
            else if (searchLower.Contains("rejected")) statusByte = 3;
            else if (searchLower.Contains("completed")) statusByte = 4;

            orders = orders.Where(o =>
                (o.Customer != null && (
                    o.Customer.FirstName.Contains(search) ||
                    o.Customer.LastName.Contains(search))) ||
                (statusByte.HasValue && o.OrderStatus == statusByte.Value));
        }

        var result = await orders
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();

        return View(result);
    }

    // GET: /Orders/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var order = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Store)
            .Include(o => o.Staff)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order == null)
            return NotFound();

        return View(order);
    }
}
