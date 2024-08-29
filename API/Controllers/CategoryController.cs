using API.Models;
using BL.DTOs.CategoryDTOs;
using BL.DTOs.ProductDTOs;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet]
    public IActionResult Get() =>
        Ok(new ResponseModel { Data = _categoryService.GetAll() });
   
    [HttpPost]
    public IActionResult Post(UpsertCategoryDTO request) =>
            Ok(new ResponseModel { Message = "Category added successfully.", Data = _categoryService.Post(request) });

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _categoryService.Delete(id);
        return Ok(new ResponseModel { Message = "Category deleted successfully." });
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, UpsertCategoryDTO request)
        => Ok(new ResponseModel { Message = "Category updated successfully.", Data = _categoryService.Put(id, request) });

}
