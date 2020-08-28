using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Yangtze.DAL.Entities;

namespace Yangtze.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public async Task<List<Category>> GetMainCategories()
        {
            using (var db = new YangtzeDBContext())
            {
                return await db.Category.Where(c => c.ParentId == null).ToListAsync();
            }
        }
        public async Task<List<Category>> GetSubCategories(int parentId)
        {
            using (var db = new YangtzeDBContext())
            {
                return await db.Category.Where(c => c.ParentId == parentId).ToListAsync();
            }
        }

        public async Task<Category> GetCategory(int categoryId)
        {
            using (var db = new YangtzeDBContext())
            {
                return await db.Category.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            }
        }

        public async Task<Category> AddCategory(Category categoryToAdd)
        {
            using (var db = new YangtzeDBContext())
            {
                db.Category.Add(categoryToAdd);
                if (await db.SaveChangesAsync() == 1)
                {
                    return categoryToAdd;
                }

                return null;
            }
        }

        public async Task<Category> UpdateCategory(Category categoryToUpdate)
        {
            using (var db = new YangtzeDBContext())
            {
                db.Category.Attach(categoryToUpdate);
                db.Entry(categoryToUpdate).State = EntityState.Modified;
                try
                {
                    await db.SaveChangesAsync();

                }

                catch (Exception ex)
                {
                    return null;
                }

                return categoryToUpdate;
            }
        }

        public async Task<Category> DeleteCategory(Category categoryToDelete)
        {
            using (var db = new YangtzeDBContext())
            {
                db.Category.Remove(categoryToDelete);

                if (await db.SaveChangesAsync() == 1)
                {
                    return categoryToDelete;
                }

                return null;
            }
        }

        public async Task<bool> CategoryInUse(int categoryId)
        {
            using (var db = new YangtzeDBContext())
            {
                var inUse = await db.Category.Include(c=>c.Product).FirstOrDefaultAsync(c => c.ParentId == categoryId || c.Product.Any(p=> p.CategoryId==categoryId));

                return inUse != null ? true : false;
            }
        }
    }
}