using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yangtze.BLL.Models;
using Yangtze.DAL.Entities;
using Yangtze.DAL.HelperModels;
using Yangtze.DAL.Repositories;

namespace Yangtze.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public IMapper _mapper { get; }

        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<(int statusCode, IEnumerable<ProductDto> Value)> GetProductsAsync(int userId, QueryStringParameters queryParams)
        {
            var result = await _repo.GetProducts(userId, queryParams);

            return (200, _mapper.Map<IEnumerable<ProductDto>>(result));
        }

        public async Task<(int statusCode, IEnumerable<ProductDto> Value)> GetProductsByCategoryAsync(int userId, int category)
        {
            var result = await _repo.GetProductsByCategory(userId, category);
            return (200, _mapper.Map<IEnumerable<ProductDto>>(result));
        }

        public async Task<(int statusCode, ProductDto Value)> GetProductByIdAsync(int userId, int productId)
        {
            var result = await _repo.GetProductById(userId, productId);
            if (result == null)
            {
                return (404, null);
            }
            return (200, _mapper.Map<ProductDto>(result));
        }

        public async Task<(int statusCode, ProductDto Value)> AddProductAsync(int userId, ProductForUpdateDto product)
        {
            if (product == null)
            {
                return (400, null);
            }

            var productToAdd = _mapper.Map<Product>(product);
            productToAdd.UserId = userId;
            productToAdd.CreatedAt = DateTime.Now;
            var result = await _repo.AddProduct(productToAdd);

            return (200, _mapper.Map<ProductDto>(result));
        }

        public async Task<(int statusCode, ProductDto Value)> UpdateProductAsync(int userId, int productId, ProductForUpdateDto product)
        {
            var productToUpdate = await _repo.GetProductByIdForUser(userId, productId);

            if (productToUpdate == null)
            {
                return (404, null);
            }

            _mapper.Map(product, productToUpdate);
            productToUpdate.UpdatedAt = DateTime.Now;
            var result = await _repo.UpdateProduct(productToUpdate);

            return (200, _mapper.Map<ProductDto>(result));
        }

        public async Task<(int statusCode, ProductDto Value)> DeleteProductAsync(int userId, int productId)
        {
            var productToDelete = await _repo.GetProductById(userId, productId);
            if(productToDelete == null)
            {
                return (404, null);
            }

            var deletedProduct = await _repo.DeleteProduct(productToDelete);

            return (200, _mapper.Map<ProductDto>(deletedProduct));
        }

    }
}
