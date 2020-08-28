using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yangtze.BLL.Models;
using Yangtze.DAL.Entities;

namespace Yangtze.BLL.Services
{
    public interface ICategoryService
    {
        Task<(int statusCode, List<CategoryDto> Value)> GetMainCategoriesAsync();
        Task<(int statusCode, List<CategoryDto> Value)> GetSubCategoriesAsync(int parentId);
        Task<(int statusCode, CategoryDto Value)> GetCategoryAsync(int categoryId);
        Task<(int statusCode, CategoryDto Value)> AddCategory(CategoryForUpdateDto categoryToAdd);
        Task<(int statusCode, CategoryDto Value)> UpdateCategory(int categoryId, CategoryForUpdateDto categoryForUpdate);
        Task<(int statusCode, CategoryDto Value)> DeleteCategory(int categoryId);
    }
}
