using BL.DTOs.CustomerDTOs;
using BL.Services.Interfaces;
using DL;
using DL.Entities;
using Shared.Helpers;

namespace BL.Services.Implementations;

public class CustomerService : ICustomerService 
{
    private ApplicationDBContext _context;

    public CustomerService(ApplicationDBContext context)
    {
        _context = context;
    }
    public GetCustomerDTO Add(UpsertCustomerDTO dto)
    {
        var customer = new Customer
        {
            CreatedDate = DateTime.Now,
            Email = dto.Email,
            PasswordHash = SecurityHelper.GenerateHash(dto.Password),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            City = dto.City,
        };

        _context.Customers.Add(customer);
        _context.SaveChanges();

        return new GetCustomerDTO
        {
            Id = customer.Id,
            Email = customer.Email,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber = customer.PhoneNumber,
            Address = customer.Address,
            City = customer.City,

        };
    }
    public GetCustomerDTO Get(int id) => _context.Customers.Select(_ => new GetCustomerDTO
    {
        Id = _.Id,
        Email = _.Email,
        FirstName = _.FirstName,
        LastName = _.LastName,
        PhoneNumber = _.PhoneNumber,
        Address = _.Address,
        City = _.City,
    }).FirstOrDefault(_ => _.Id == id) ?? throw new Exception("There is no such customer found.");
    public GetCustomerDTO Login(string email, string password)
    {
        
        var customer = _context.Customers.FirstOrDefault(c=> c.Email == email);
        if (customer == null)
            throw new Exception("No user found.");
        else if(!SecurityHelper.ValidateHash(password, customer.PasswordHash))
            throw new Exception("Invalid credentials.");


        return new GetCustomerDTO
        {
            Id = customer.Id,
            Email = customer.Email,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber = customer.PhoneNumber,
            Address = customer.Address,
            City = customer.City,
        };
    }
    private GetCustomerDTO Get(string email, string password)
    {
        var customer = _context.Customers.FirstOrDefault(_ => _.Email == email);
        if (customer == null)
            throw new Exception("customer not found.");

        return new GetCustomerDTO
        {
            Id = customer.Id,
            Email = customer.Email,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber = customer.PhoneNumber,
            Address = customer.Address,
            City = customer.City,
        };
    }
}
