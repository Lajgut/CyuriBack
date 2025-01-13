using Cuyri.Controller;
using Cuyri.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cuyri.Tests.Controller;

public class UsersControllerTests
{
    private readonly DbContextOptions<ApplicationDbContext> _options =
        new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

    [Fact]
    public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new UsersController(context);

            var result = await controller.GetUser(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }

    [Fact]
    public async Task GetUser_ReturnsUser_WhenUserExists()
    {
        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new UsersController(context);

            context.Users.Add(new User { Id = 1, Username = "Test" });
            await context.SaveChangesAsync();
            
            var result = await controller.GetUser(1);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var user = Assert.IsType<User>(okResult.Value);
            Assert.Equal("Test", user.Username);
            Assert.Equal(1, user.Id);
        }
    }

    [Fact]
    public async Task CreateUser_ReturnsCreatedAtAction_WhenUserIsCreated()
    {
        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new UsersController(context);
            
            var newUser = new User { Id = 1, Username = "NewUser" };
            
            var result = await controller.AddUser(newUser);
            
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var user = Assert.IsType<User>(createdAtActionResult.Value);
            Assert.Equal(1, user.Id);
            Assert.Equal("NewUser", user.Username);
            
            var userInDb = await context.Users.FindAsync(1);
            Assert.NotNull(userInDb);
            Assert.Equal("NewUser", userInDb.Username);
        }
    }
}