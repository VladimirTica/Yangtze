using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yangtze.DAL.Entities;

namespace Yangtze.DAL.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetMainCategories();
        Task<List<Category>> GetSubCategories(int parentId);
        Task<Category> GetCategory(int categoryId);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<Category> DeleteCategory(Category category);
        Task<bool> CategoryInUse(int categoryId);
    }
}
