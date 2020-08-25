using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yangtze.BLL.Models;

namespace Yangtze.BLL.Services
{
    public interface IYangtzeService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(int userId);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int userId, int category);
        Task<ProductDto> GetProductByIdAsync(int userId, int productId);
        Task<ProductDto> AddProductAsync(int userId, ProductForUpdateDto product);
        Task<ProductDto> UpdateProductAsync(int userId, int productId, ProductForUpdateDto product);
        Task<ProductDto> DeleteProductAsync(int userId, int productId);
    }
}
