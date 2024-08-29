using DL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Entities;

public class Customer : BaseEntity
{
    public string FirstName { get; set; } = String.Empty; // Customer's first name
    public string LastName { get; set; } = String.Empty; // Customer's last name
    public string Email { get; set; } = String.Empty; // Customer's email address
    public string PhoneNumber { get; set; } = String.Empty; // Customer's phone number
    public string Address { get; set; } = String.Empty;// Customer's address
    public string City {  get; set; } = String.Empty;
    public string PasswordHash { get; set; } = String.Empty;// Customer's address

    // Navigation property to Orders
    public ICollection<Order> Orders { get; set; }
}
