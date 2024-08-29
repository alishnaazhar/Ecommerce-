namespace BL.DTOs.CouponDTOs;

public class GetCouponDTO : BaseEntityDTO
{
    public string Name { get; set; } = String.Empty;
    public string Code { get; set; } = String.Empty;
    public decimal Discount { get; set; }
    public DateTime Expiration_Date { get; set; }

}
