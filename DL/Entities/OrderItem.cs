using DL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DL.Entities;

public class OrderItem : BaseEntity
{
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public Order Order { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }
}
