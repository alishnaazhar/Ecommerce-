using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.ProductDTOs;

public class UpsertProductDTO
{
    public string Name { get; set; } = String.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = String.Empty;
    public int QuantityAvailable { get; set; }
    public int CategoryId { get; set; }

}
