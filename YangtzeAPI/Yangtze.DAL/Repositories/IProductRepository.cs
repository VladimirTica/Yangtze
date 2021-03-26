using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yangtze.DAL.Entities;
using Yangtze.DAL.HelperModels;

namespace Yangtze.DAL.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(int userId, QueryStringParameters queryParams);
        Task<List<Product>> GetProductsByCategory(int userId, int categoryId);
        Task<Product> GetProductById(int userId, int id);
        Task<Product> GetProductByIdForUser(int userId, int productId);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(Product product);
    }
}
