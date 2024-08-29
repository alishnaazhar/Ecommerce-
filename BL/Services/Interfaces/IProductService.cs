using BL.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces;

public interface IProductService
{
    List<GetProductDTO> GetAll();
    GetProductDTO Get(int id);
    GetProductDTO Post(UpsertProductDTO dto);
    GetProductDTO Put(int id, UpsertProductDTO dto);
    void Delete(int id);

}
