using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.CustomerDTOs;

public class  GetCustomerDTO
{
    public int Id { get; set; } 
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public string City { get; set; } = String.Empty;

    public string Email { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
}
