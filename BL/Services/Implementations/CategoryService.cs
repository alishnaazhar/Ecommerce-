using BL.DTOs.CategoryDTOs;
using BL.DTOs.OrderDTOs;
using BL.DTOs.ProductDTOs;
using BL.Services.Interfaces;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Implementations;

public class CategoryService : ICategoryService
{
    private ApplicationDBContext _context;

    public CategoryService(ApplicationDBContext context)
    {
        _context = context;
    }
    public List<GetCategoryDTO> GetAll()
    {
        var categories = _context.Categories.ToList();
        List<GetCategoryDTO> categoryList = new List<GetCategoryDTO>();
        foreach (var category in categories)
        {
            var newCategory = new GetCategoryDTO
            {
                CategoryName = category.Name,
                Description = category.Description,
                CreatedDate = DateTime.Now,
                Id = category.Id,
            };
            categoryList.Add(newCategory);
        }
        return categoryList;

    }
    public void Delete(int id)
    {
        var category = _context.Categories.FirstOrDefault(_ => _.Id == id);
        if (category == null)
            throw new Exception("There is no such category found.");

        _context.Categories.Remove(category);
        _context.SaveChanges();
    }
    public GetCategoryDTO Post(UpsertCategoryDTO dto)
    {
        var category = new Category
        {
            Name=dto.CategoryName,
            Description=dto.Description,
            CreatedDate = DateTime.Now,
        };
        _context.Categories.Add(category);
        _context.SaveChanges();

        return new GetCategoryDTO
        {
            CategoryName = category.Name,
            Description = category.Description,
            CreatedDate = DateTime.Now,
            Id = category.Id,
        };
    }
    public GetCategoryDTO Put(int id, UpsertCategoryDTO dto)
    {
        var category = _context.Categories.FirstOrDefault(_ => _.Id == id);
        if (category == null)
            throw new Exception("There is no such category found.");

        category.UpdatedDate = DateTime.Now;
        category.Name = dto.CategoryName;
        category.Description = dto.Description;
        _context.SaveChanges();

        return new GetCategoryDTO
        {
            CategoryName = category.Name,
            Description = category.Description,
            CreatedDate = category.CreatedDate,
            UpdatedDate = category.UpdatedDate,
            Id = category.Id,
        };
    }

  
}
