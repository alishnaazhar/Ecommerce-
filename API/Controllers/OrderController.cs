using API.Models;
using BL.DTOs.CategoryDTOs;
using BL.DTOs.OrderDTOs;
using BL.Services.Implementations;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    [HttpGet("{id:int}")]
    public IActionResult Get(int id) =>
        Ok(new ResponseModel { Data = _orderService.Get(id) });

    [HttpPost,Authorize]
    public async Task<IActionResult> Post(UpsertOrderDTO request) =>
            Ok(new ResponseModel { Message = "Order added successfully.", Data = await _orderService.Post(request) });
   
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _orderService.Delete(id);
        return Ok(new ResponseModel { Message = "Order deleted successfully." });
    }

    [HttpPut("{id}"), Authorize]
    public IActionResult Put(int id, UpsertOrderDTO request)
        => Ok(new ResponseModel { Message = "Order updated successfully.", Data = _orderService.Put(id, request) });

}
