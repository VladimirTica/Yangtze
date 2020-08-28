using AutoMapper;
using System;
using System.Collections.Generic;
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
            var result = await _repo.GetMainCategories();
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

            return (200, _mapper.Map<CategoryDto>(result));
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
    }
}
