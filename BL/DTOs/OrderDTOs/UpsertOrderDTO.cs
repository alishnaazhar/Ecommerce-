using BL.DTOs.OrderItemsDTO;
using DL.Enumerations;

namespace BL.DTOs.OrderDTOs;

public class UpsertOrderDTO
{

    public string Firstname { get; set; } = String.Empty;
    public string Lastname { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public string City { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public enPaymentMethod PaymentMethod { get; set; }

    public List<AddOrderItemDTO> OrderItems { get; set; }
    public string Coupon { get; set; } = null!;

}
