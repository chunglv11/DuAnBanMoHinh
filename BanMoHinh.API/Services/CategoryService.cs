using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using System.Data.Entity;

namespace BanMoHinh.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly MyDbContext _dbContext;

        public CategoryService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Create(Category item)
        {
            try
            {
                var category = new Category()
                {
                    Id = item.Id,
                    CategoryName = item.CategoryName,
                };
                await _dbContext.Category.AddAsync(category);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var item = await _dbContext.Category.FirstOrDefaultAsync(c => c.Id == id);
                _dbContext.Category.Remove(item);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<List<Category>> GetAll()
        {
            return await _dbContext.Category.ToListAsync();
        }

        public async Task<Category> GetItem(Guid id)
        {
            return await _dbContext.Category.FindAsync(id);
        }

        public async Task<bool> Update(Guid id, Category item)
        {
            try
            {
                var category = await _dbContext.Category.FirstOrDefaultAsync(c => c.Id == id);
                category.CategoryName = item.CategoryName;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
