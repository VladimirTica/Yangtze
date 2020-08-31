using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yangtze.BLL.Models;
using Yangtze.DAL.Entities;
using Yangtze.DAL.Repositories;

namespace Yangtze.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<(int statusCode, List<CategoryDto> Value)> GetMainCategoriesAsync()
        {
            var result = await _repo.GetAllCategories();
            return(200, _mapper.Map<List<CategoryDto>>(result));
        }

        public async Task<(int statusCode, List<CategoryDto> Value)> GetSubCategoriesAsync(int parentId)
        {
            var result = await _repo.GetSubCategories(parentId);
            return (200, _mapper.Map<List<CategoryDto>>(result));
        }


        public async Task<(int statusCode, CategoryDto Value)> GetCategoryAsync(int categoryId)
        {
            var result = await _repo.GetCategory(categoryId);

            if (result == null)
            {
                return (404, null);
            }

            var subcategories = await _repo.GetSubCategories(categoryId);

            var categoryWithSubcategories = _mapper.Map<CategoryDto>(result);
            categoryWithSubcategories.InverseParent = _mapper.Map<ICollection<CategoryDto>>(subcategories);

            return (200, categoryWithSubcategories);
        }

        public async Task<(int statusCode, CategoryDto Value)> AddCategory(CategoryForUpdateDto category)
        {

            if (category == null)
            {
                return (400, null);
            }

            var categoryToAdd = _mapper.Map<Category>(category);
            var result = await _repo.AddCategory(categoryToAdd);

            return (201, _mapper.Map<CategoryDto>(result));
        }

        public async Task<(int statusCode, CategoryDto Value)> UpdateCategory(int categoryId, CategoryForUpdateDto category)
        {
            var categoryToUpdate = await _repo.GetCategory(categoryId);

            if (categoryToUpdate == null)
            {
                return (404, null);
            }

            _mapper.Map(category, categoryToUpdate);

            var allCategories = await _repo.GetAllCategories();
            var isParentValid = ValidParentCategory(categoryId, categoryToUpdate.ParentId, allCategories);
            if (!isParentValid)
            {
                return (400, null);
            }

            var updatedCategory = await _repo.UpdateCategory(categoryToUpdate);

            return (200, _mapper.Map<CategoryDto>(updatedCategory));
        }

        public async Task<(int statusCode, CategoryDto Value)> DeleteCategory(int categoryId)
        {
            var categoryToDelete = await _repo.GetCategory(categoryId);
            if (categoryToDelete == null)
            {
                return (404, null);
            }
            var categoryInUse = await _repo.CategoryInUse(categoryId);

            if (categoryInUse)
            {
                return (400, null);
            }

            var deletedCategory = await _repo.DeleteCategory(categoryToDelete);

            return (200, _mapper.Map<CategoryDto>(deletedCategory));
        }

        public bool ValidParentCategory(int categoryId, int? parentId, List<Category> categories)
        {
            bool result = true;

            if (!parentId.HasValue)
            {
                return true;
            }

            if(categoryId == parentId)
            {
                return false;
            }

            var subcategories = categories.Where(c => c.ParentId == categoryId).ToList();
            if (subcategories.Any(c => c.CategoryId == parentId))
            {
                return false;
            }

            if (!subcategories.Any())
            {
                return true;
            }

            foreach (var category in subcategories)
            {
                result = ValidParentCategory(category.CategoryId, parentId, categories);
            }

            return result;
        }
    }
}
