using API.Models;
using BL.DTOs.CustomerDTOs;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private IConfiguration _config;
    private ICustomerService _customerService;

    public CustomerController(IConfiguration config, ICustomerService customerService)
    {
        _config = config;
        _customerService = customerService;
    }

    [HttpPost("register")]
    public IActionResult Register(UpsertCustomerDTO request)
    {
        var customer = _customerService.Add(request);
        return Ok(new ResponseModel { Message = "Customer register successfully.", Data = customer });
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDTO request)
        => Ok(new ResponseModel { Data = CreateToken(_customerService.Login(request.Email, request.Password)) });

    private string CreateToken(GetCustomerDTO customer)
    {
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, customer.Email),
            new Claim(ClaimTypes.Name, customer.Email),
            new Claim(ClaimTypes.UserData, JsonSerializer.Serialize(customer)),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _config.GetSection("JWT:SecretKey").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
