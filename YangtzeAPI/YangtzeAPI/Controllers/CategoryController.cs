using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yangtze.BLL.Models;
using Yangtze.BLL.Services;
using YangtzeAPI.Helper;

namespace YangtzeAPI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ResponseWrapper
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet(Name = "GetCategories")]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            var result = await _service.GetMainCategoriesAsync();

            return ResponseGet(result);
        }

        [HttpGet("{parentId}/subcategories/", Name = "GetSubcategories")]
        public async Task<ActionResult<List<CategoryDto>>> GetSubcategories(int parentId)
        {
            var result = await _service.GetSubCategoriesAsync(parentId);

            return ResponseGet(result);
        }


        [HttpGet("{categoryId}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int categoryId)
        {
            var result = await _service.GetCategoryAsync(categoryId);
            if (result.Value == null)
            {
                return ResponseGet(result, $"Category with id of {categoryId} does not exist.");
            }

            return ResponseGet(result);
        }

        [HttpPost(Name = "CreateCategory")]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CategoryForUpdateDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _service.AddCategory(category);

            if (result.Value == null)
            {
                return BadRequest("Failed to create category.");
            }

            return ResponseGet(result);
        }

        [HttpPut("{categoryId}", Name = "UpdateCategpry")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(int categoryId, [FromBody] CategoryForUpdateDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _service.UpdateCategory(categoryId, category);

            if (result.Value == null && result.statusCode == 404)
            {
                return ResponseGet(result, $"Category with id of {categoryId} does not exist.");
            }

            else if (result.Value == null && result.statusCode == 400)
            {
                return ResponseGet(result, $"Failed to update category. Inappropriate parentId with value of {category.ParentId}.");
            }

            return ResponseGet(result);
        }

        [HttpDelete("{categoryId}", Name = "DeleteCategory")]
        public async Task<ActionResult<CategoryDto>> DeleteCategory(int categoryId)
        {
            var result = await _service.DeleteCategory(categoryId);
            if (result.Value == null && result.statusCode == 404)
            {
                return ResponseGet(result, $"Category with id of {categoryId} does not exist.");
            }

            else if (result.Value == null && result.statusCode == 400)
            {
                return ResponseGet(result, $"Failed to delete. Category is assigned as a parent category.");
            }

            return ResponseGet(result);
        }
    }
}
