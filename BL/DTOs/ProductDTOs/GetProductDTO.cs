using DL.Entities;
using System;

namespace BL.DTOs.ProductDTOs;

public class GetProductDTO : BaseEntityDTO
{
    public string Name { get; set; } = String.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = String.Empty;
    public int QuantityAvailable { get; set; }
    public int CategoryId { get; set; }

    public List<OrderItem> OrderItems { get; set; }
}
