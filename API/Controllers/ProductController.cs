using API.Models;
using BL.DTOs.ProductDTOs;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    [HttpGet]
    public IActionResult Get() =>
        Ok(new ResponseModel { Data = _productService.GetAll() });

    [HttpGet("{id:int}")]
    public IActionResult Get(int id) =>
        Ok(new ResponseModel { Data = _productService.Get(id) });


    [HttpPost]
    public IActionResult Post(UpsertProductDTO request) =>
        Ok(new ResponseModel { Message = "Product added successfully.", Data = _productService.Post(request) });

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _productService.Delete(id);
        return Ok(new ResponseModel { Message = "Product deleted successfully." });
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, UpsertProductDTO request)
        => Ok(new ResponseModel { Message = "Product updated successfully.", Data = _productService.Put(id, request) });

}
