using DL.Entities;
using DL.Enumerations;

namespace BL.DTOs.OrderDTOs;

public class GetOrderDTO : BaseEntityDTO
{

    public decimal TotalAmount { get; set; }
    public string Firstname { get; set; } = String.Empty;
    public string Lastname { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public string City { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public enPaymentMethod PaymentMethod { get; set; }
    public int CustomerId { get; set; }
    //public List<OrderItem> Items { get; set; }
}
