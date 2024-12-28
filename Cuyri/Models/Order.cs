using System.ComponentModel.DataAnnotations;

namespace Cuyri.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }
    [Required]
    [MaxLength(50)]
    public string OrderName { get; set; }
    [Required]
    public string UserId { get; set; }
}