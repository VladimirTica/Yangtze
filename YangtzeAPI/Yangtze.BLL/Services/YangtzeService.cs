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
    public class YangtzeService : IYangtzeService
    {
        private readonly IYangtzeRepository _repo;

        public IMapper _mapper { get; }

        public YangtzeService(IYangtzeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(int userId)
        {
            var result = await _repo.GetProducts(userId);

            return _mapper.Map<IEnumerable<ProductDto>>(result);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int userId, int category)
        {
            var result = await _repo.GetProductsByCategory(userId, category);
            return _mapper.Map<IEnumerable<ProductDto>>(result);
        }

        public async Task<ProductDto> GetProductByIdAsync(int userId, int productId)
        {
            var result = await _repo.GetProductById(userId, productId);

            return _mapper.Map<ProductDto>(result);
        }

        public async Task<ProductDto> AddProductAsync(int userId, ProductForUpdateDto product)
        {
            if (product == null)
            {
                return null;
            }

            var productToAdd = _mapper.Map<Product>(product);
            productToAdd.UserId = userId;
            productToAdd.CreatedAt = DateTime.Now;
            var result = await _repo.AddProduct(productToAdd);

            return _mapper.Map<ProductDto>(result);
        }

        public async Task<ProductDto> UpdateProductAsync(int userId, int productId, ProductForUpdateDto product)
        {
            var productToUpdate = await _repo.GetProductByIdForUser(userId, productId);

            if (productToUpdate == null)
            {
                return null;
            }

            _mapper.Map(product, productToUpdate);
            productToUpdate.UpdatedAt = DateTime.Now;
            var result = await _repo.UpdateProduct(productToUpdate);

            return _mapper.Map<ProductDto>(result);
        }

        public async Task<ProductDto> DeleteProductAsync(int userId, int productId)
        {
            var productToDelete = await _repo.GetProductById(userId, productId);
            if(productToDelete == null)
            {
                return null;
            }

            var deletedProduct = await _repo.DeleteProduct(productToDelete);

            return _mapper.Map<ProductDto>(deletedProduct);
        }

    }
}
