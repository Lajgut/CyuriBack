using Cuyri.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cuyri.Controller;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(ApplicationDbContext applicationDbContext) : ControllerBase
{
    [HttpGet("/orders")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        if (!applicationDbContext.Orders.Any()) return NotFound();
        return await applicationDbContext.Orders.ToListAsync();
    }

    [HttpGet("getById")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await applicationDbContext.Orders.FindAsync(id);
        if (order == null) return NotFound();
        return order;
    }

    [HttpGet("getByUserId")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrder(string userId)
    {
        var orders = await applicationDbContext.Orders.Where(x => x.UserId == userId).ToListAsync();
        if (orders.Count == 0) return NotFound();
        return orders;
    }

    [HttpPost("/setOrder")]
    public async Task<ActionResult<Order>> PostOrder(Order order)
    {
        applicationDbContext.Orders.Add(order);
        await applicationDbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
    }
}