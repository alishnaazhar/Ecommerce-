using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.CategoryDTOs;

public class GetCategoryDTO : BaseEntityDTO
{
    public string CategoryName { get; set; }= String.Empty;
    public string Description { get; set; }=String.Empty;
}
