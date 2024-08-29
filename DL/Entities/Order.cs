using DL.Entities.Base;
using DL.Enumerations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DL.Entities;

public class Order : BaseEntity
{
    public string Firstname { get; set; } = String.Empty;
    public string Lastname { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public string City { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public decimal TotalAmount { get; set; }
    public enPaymentMethod PaymentMethod { get; set; }

    [ForeignKey("Customer")]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public List<OrderItem> Items { get; set; }
}
