using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yangtze.DAL.Entities;

namespace Yangtze.DAL.Repositories
{
    public class YangtzeRepository : IYangtzeRepository
    {
        public async Task<List<Product>> GetProducts(int userId)
        {
            using (var db = new YangtzeDBContext())
            {
                return await db.Product.Where(p=> p.UserId==userId).ToListAsync();
            }
        }

        public async Task<List<Product>> GetProductsByCategory(int userId, int categoryId)
        {
            using (var db = new YangtzeDBContext())
            {
                return await db.Product.Where(p => p.UserId==userId && p.CategoryId == categoryId).ToListAsync();
            }
        }

        public async Task<Product> GetProductById(int userId, int id)
        {
            using (var db = new YangtzeDBContext())
            {
                return await db.Product.FirstOrDefaultAsync(p => p.UserId == userId && p.ProductId == id);
            }
        }

        public async Task<Product> GetProductByIdForUser(int userId, int productId)
        {
            using (var db = new YangtzeDBContext())
            {
                return await db.Product.FirstOrDefaultAsync(p =>p.UserId==userId && p.ProductId == productId);
            }
        }

        public async Task<Product> AddProduct(Product productToAdd)
        {
            using (var db = new YangtzeDBContext())
            {
                db.Product.Add(productToAdd);
                if (await db.SaveChangesAsync()==1)
                {
                    return productToAdd;
                }

                return null;
            }
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            using (var db = new YangtzeDBContext())
            {
                db.Product.Attach(product);
                db.Entry(product).State = EntityState.Modified;
                try
                {
                    await db.SaveChangesAsync();

                }

                catch(Exception ex)
                {
                    return null;
                }

                return product;
            }
        }

        public async Task<Product> DeleteProduct(Product product)
        {
            using (var db = new YangtzeDBContext())
            {
                db.Product.Remove(product);
                if (await db.SaveChangesAsync() == 1)
                {
                    return product;
                }

                return null;
            }
        }
    }
}
