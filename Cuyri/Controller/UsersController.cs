using Cuyri.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cuyri.Controller;

[ApiController]
[Route("api/[controller]")]
public class UsersController(ApplicationDbContext context): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await context.Users.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }
}