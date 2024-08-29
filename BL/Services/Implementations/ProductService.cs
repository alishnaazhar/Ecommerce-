using BL.DTOs.ProductDTOs;
using BL.Services.Interfaces;
using DL;
using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Implementations;

public class ProductService : IProductService 
{

    private ApplicationDBContext _context;
    public ProductService(ApplicationDBContext context)
    {
        _context = context;
    }
   
    public void Delete(int id)
    {
        var product = _context.Products.FirstOrDefault(_ => _.Id == id);
        if (product == null)
            throw new Exception("There is no such product found.");

        _context.Products.Remove(product);
        _context.SaveChanges();
    }

    public GetProductDTO Get(int id)
    {
        var product = _context.Products.FirstOrDefault(_ => _.Id == id)
                    ?? throw new Exception("There is no such product found.");

        return new GetProductDTO
        {
            Id = id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            QuantityAvailable= product.QuantityAvailable,
            CreatedDate = product.CreatedDate,
            CategoryId = product.CategoryId,
            OrderItems = product.OrderItems,
        };
    }

       
    public List<GetProductDTO> GetAll()
    {
        var Products = _context.Products.ToList();
        List<GetProductDTO> ProductList = new List<GetProductDTO>();
        foreach (var product in Products)
        {
            var newProduct = new GetProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                CreatedDate = product.CreatedDate,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                QuantityAvailable = product.QuantityAvailable,
                OrderItems = product.OrderItems,

            };
            ProductList.Add(newProduct);
        }
        return ProductList;
    }

    public GetProductDTO Post(UpsertProductDTO dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = ConversionHelper.ConvertTo<decimal>(dto.Price),
            Description = dto.Description,
            QuantityAvailable = dto.QuantityAvailable,
            CreatedDate= DateTime.Now,
            CategoryId = dto.CategoryId
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        return new GetProductDTO
        {
            Id=product.Id,
            CreatedDate = DateTime.Now,
            Name= product.Name,
            Price=product.Price,
            Description= product.Description,
            CategoryId= dto.CategoryId,
            QuantityAvailable= dto.QuantityAvailable,
        };
    }

    public GetProductDTO Put(int id, UpsertProductDTO dto)
    {
        var product = _context.Products.FirstOrDefault(_ => _.Id == id);
        if (product == null)
            throw new Exception("There is no such product found.");

        product.Name = dto.Name;
        product.Price = ConversionHelper.ConvertTo<decimal>(dto.Price);
        product.Description = dto.Description;
        product.UpdatedDate= DateTime.Now;
        product.CategoryId = dto.CategoryId;

        _context.SaveChanges();

        return new GetProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description= product.Description,
            QuantityAvailable = dto.QuantityAvailable,
            UpdatedDate= DateTime.Now,
            CreatedDate= product.CreatedDate,
            CategoryId= dto.CategoryId
        };
    }
}
