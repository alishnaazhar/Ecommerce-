using BL.DTOs.CustomerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces;

public interface ICustomerService
{
    GetCustomerDTO Add(UpsertCustomerDTO dto);
    GetCustomerDTO Get(int id);
    GetCustomerDTO Login(string email, string password);
}
