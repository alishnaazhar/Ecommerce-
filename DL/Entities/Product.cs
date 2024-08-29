using DL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DL.Entities;
public class Product : BaseEntity
{
    public string Name { get; set; } = String.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = String.Empty;
    public int QuantityAvailable { get; set; }
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}


