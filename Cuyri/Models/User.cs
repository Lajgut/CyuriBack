namespace Cuyri.Models;

public class User
{
    public int Id { get; set; }
    // че с полями?
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public required string Role { get; set; }
}