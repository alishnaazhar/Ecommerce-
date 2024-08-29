using API.Models;
using BL.DTOs.CouponDTOs;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponController : ControllerBase
{

    private ICouponService _couponService;

    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    [HttpGet]
    public IActionResult Get() =>
        Ok(new ResponseModel { Data = _couponService.GetAll() });

    [HttpGet("{id:int}")]
    public IActionResult Get(int id) =>
        Ok(new ResponseModel { Data = _couponService.Get(id) });

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _couponService.Delete(id);
        return Ok(new ResponseModel { Message = "Coupon deleted successfully." });
    }

    [HttpPost]
    public IActionResult Post(UpsertCouponDTO request) =>
    Ok(new ResponseModel { Message = "Coupon added successfully.", Data = _couponService.Post(request) });

    [HttpPut("{id}")]
    public IActionResult Put(int id, UpsertCouponDTO request)
        => Ok(new ResponseModel { Message = "Coupon updated successfully.", Data = _couponService.Put(id, request) });



}
