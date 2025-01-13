namespace Cuyri.Models;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public string? PasswordHash { get; set; }
    public string? Role { get; set; }
}