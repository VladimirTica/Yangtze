using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yangtze.BLL.Models;
using Yangtze.DAL.HelperModels;

namespace Yangtze.BLL.Services
{
    public interface IProductService
    {
        Task<(int statusCode, IEnumerable<ProductDto> Value)> GetProductsAsync(int userId, QueryStringParameters queryParams);
        Task<(int statusCode, IEnumerable<ProductDto> Value)> GetProductsByCategoryAsync(int userId, int category);
        Task <(int statusCode, ProductDto Value)> GetProductByIdAsync(int userId, int productId);
        Task<(int statusCode, ProductDto Value)> AddProductAsync(int userId, ProductForUpdateDto product);
        Task<(int statusCode, ProductDto Value)> UpdateProductAsync(int userId, int productId, ProductForUpdateDto product);
        Task<(int statusCode, ProductDto Value)> DeleteProductAsync(int userId, int productId);
    }
}
