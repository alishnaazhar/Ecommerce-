using BL.DTOs.CategoryDTOs;

namespace BL.Services.Interfaces;

public interface ICategoryService
{
    List<GetCategoryDTO> GetAll();
    GetCategoryDTO Post(UpsertCategoryDTO dto);
    GetCategoryDTO Put(int id, UpsertCategoryDTO dto);

    void Delete(int id);
}
