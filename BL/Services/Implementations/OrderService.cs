using BL.DTOs.OrderDTOs;
using BL.Services.External;
using BL.Services.Interfaces;
using DL;
using DL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BL.Services.Implementations;

public class OrderService :IOrderService 
{
    private ApplicationDBContext _context;
    private EmailService _emailService;
    private IStateHelper _stateHelper;


    public OrderService(ApplicationDBContext context, EmailService emailService, IStateHelper stateHelper)
    {
        _context = context;
        _emailService = emailService;
        _stateHelper = stateHelper;
    }
    public void Delete(int id)
    {
        var order = _context.Orders.FirstOrDefault(_ => _.Id == id);
        if (order == null)
            throw new Exception("There is no such order found.");

        _context.Orders.Remove(order);
        _context.SaveChanges();
    }
    public GetOrderDTO Get(int id)
    {
        var order = _context.Orders.FirstOrDefault(_ => _.Id == id)
                    ?? throw new Exception("There is no such order found.");

        return new GetOrderDTO
        {
            Id = order.Id,
            Email = order.Email,
            CreatedDate = order.CreatedDate,
            UpdatedDate = order.UpdatedDate,
            CustomerId = order.CustomerId,
            Firstname = order.Firstname,
            Lastname = order.Lastname,
            Address = order.Address,
            City = order.City,
            PhoneNumber = order.PhoneNumber,
            //Items = order.Items,
            TotalAmount = order.TotalAmount,
            PaymentMethod = order.PaymentMethod,
        };
    }

    public async Task<GetOrderDTO> Post(UpsertOrderDTO dto)
    {
        decimal discount = 0;
        if (!string.IsNullOrEmpty(dto.Coupon))
        {
            var coupon = _context.Coupons.FirstOrDefault(_ => _.Code == dto.Coupon);
            if (coupon == null)
                throw new Exception("Coupon does not exist");
            discount = coupon.Discount;
        }
        var product = _context.Products
            .Where(p => dto.OrderItems.Select(o => o.ProductId).Contains(p.Id))
            .Select(p => new
            {
                Id = p.Id,
                Price = p.Price
            })
            .ToList();
        var totalAmount = (from p in product
                           join o in dto.OrderItems
                           on p.Id equals o.ProductId
                           select new
                           {
                               totalAmount = p.Price * o.Quantity
                           }).Sum(_ => _.totalAmount);
        if (discount > 0)
        {
            totalAmount = totalAmount - ((totalAmount / 100) * discount);
        }

        var order = new Order
        {
            CreatedDate = DateTime.Now,
            CustomerId= _stateHelper.User().Id,
            Firstname = dto.Firstname,
            Lastname = dto.Lastname,
            Email = dto.Email,
            Address = dto.Address,
            City = dto.City,
            PhoneNumber = dto.PhoneNumber,
            TotalAmount = totalAmount,
            PaymentMethod = dto.PaymentMethod,
        };
        _context.Orders.Add(order);
        _context.SaveChanges();

        List<OrderItem> orderItems = new List<OrderItem>();
        foreach (var orderItem in dto.OrderItems)
        {
            orderItems.Add(new OrderItem
            {
                OrderId = order.Id,
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
            });
        }
        _context.OrderItems.AddRange(orderItems);

        _context.SaveChanges();

        await _emailService.SendEmailAsync(dto.Email, "Order Confirmation", $"Your order#{order.Id} has been processed");

        return new GetOrderDTO
        {
            Id = order.Id,
            Firstname = order.Firstname,
            Lastname = order.Lastname,
            Email = order.Email,
            Address = order.Address,
            City = order.City,
            PhoneNumber = order.PhoneNumber,
            CreatedDate = DateTime.Now,
            TotalAmount = order.TotalAmount,
            PaymentMethod = order.PaymentMethod,
        };
    }

    public GetOrderDTO Put(int id, UpsertOrderDTO dto)
    {
        var order = _context.Orders.FirstOrDefault(_ => _.Id == id);
        if (order == null)
            throw new Exception("There is no such order found.");

        decimal discount = 0;
        if (!string.IsNullOrEmpty(dto.Coupon))
        {
            var coupon = _context.Coupons.FirstOrDefault(_ => _.Code == dto.Coupon);
            if (coupon == null)
                throw new Exception("Coupon does not exist");
            discount = coupon.Discount;
        }
        var product = _context.Products
            .Where(p => dto.OrderItems.Select(o => o.ProductId).Contains(p.Id))
            .Select(p => new
            {
                Id = p.Id,
                Price = p.Price
            })
            .ToList();
        var totalAmount = (from p in product
                           join o in dto.OrderItems
                           on p.Id equals o.ProductId
                           select new
                           {
                               totalAmount = p.Price * o.Quantity
                           }).Sum(_ => _.totalAmount);
        if (discount > 0)
        {
            totalAmount = totalAmount - ((totalAmount / 100) * discount);
        }

        order.Firstname = dto.Firstname;
        order.Lastname = dto.Lastname;
        order.Email = dto.Email;
        order.Address = dto.Address;
        order.City = dto.City;
        order.PhoneNumber = dto.PhoneNumber;
        order.PaymentMethod = dto.PaymentMethod;
        order.TotalAmount = totalAmount;
        _context.SaveChanges();

        _context.OrderItems.RemoveRange(_context.OrderItems.Where(oi => oi.OrderId == order.Id));

        List<OrderItem> orderItems = new List<OrderItem>();
        foreach (var orderItem in dto.OrderItems)
        {
            orderItems.Add(new OrderItem
            {
                OrderId = order.Id,
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
            });
        }
        _context.OrderItems.AddRange(orderItems);

        _context.SaveChanges();
        return new GetOrderDTO
        {
            Id = order.Id,
            UpdatedDate = DateTime.Now,
            CustomerId = _stateHelper.User().Id,
            Firstname = order.Firstname,
            Lastname = order.Lastname,
            Email = order.Email,
            Address = order.Address,
            City = order.City,
            PhoneNumber = order.PhoneNumber,
            CreatedDate = order.CreatedDate,    
            //Items = orderItems,
            TotalAmount = order.TotalAmount,
            PaymentMethod = order.PaymentMethod,
        };
    }
}
