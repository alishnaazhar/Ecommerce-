using DL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Entities;

public class Coupon : BaseEntity
{
public string Name { get; set; } = String.Empty;
public string Code { get; set; } = String.Empty;
public decimal Discount { get; set; }
public DateTime Expiration_Date { get; set; }
}
