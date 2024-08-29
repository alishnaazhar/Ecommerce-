using BL.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces;

public interface IOrderService
{
    GetOrderDTO Get(int id);
    Task<GetOrderDTO> Post(UpsertOrderDTO dto);
    GetOrderDTO Put(int id, UpsertOrderDTO dto);
    void Delete(int id);
}
