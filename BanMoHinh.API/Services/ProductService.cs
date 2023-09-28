using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BanMoHinh.API.Services
{
    public class ProductService : IProductService
    {
        private MyDbContext _dbContext;
        // private cate, bran, mate
        public ProductService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(ProductVM item)
        {

            try
            {
                Product pr = new Product()
                {
                    Id = Guid.NewGuid(),
                    CategoryId = item.CategoryId,
                    BrandId = item.BrandId,
                    MaterialId = item.MaterialId,
                    ProductName = item.ProductName,
                    AvailableQuantity = item.AvailableQuantity,
                    Create_At = item.Create_At,
                    Update_At = item.Update_At,
                    Description = item.Description,
                    Long_Description = item.Long_Description,
                    Status = item.Status,
                };
                await _dbContext.Product.AddAsync(pr);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> CreateMany(List<ProductVM> items)
        {
            try
            {
                foreach (var x in items)
                {
                    Product pr = new Product()
                    {
                        Id = Guid.NewGuid(),
                        CategoryId = x.CategoryId,
                        BrandId = x.BrandId,
                        MaterialId = x.MaterialId,
                        ProductName = x.ProductName,
                        AvailableQuantity = x.AvailableQuantity,
                        Create_At = x.Create_At,
                        Update_At = x.Update_At,
                        Description = x.Description,
                        Long_Description = x.Long_Description,
                        Status = x.Status,
                    };
                    await _dbContext.AddRangeAsync(pr);
                }
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
                var idp = await _dbContext.Product.FindAsync(id);
                _dbContext.Remove(idp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dbContext.Product.ToListAsync();
        }

        public async Task<Product> GetItem(Guid id)
        {
            return await _dbContext.Product.FindAsync(id);
        }

        public async Task<bool> Update(Guid id, ProductVM item)
        {
            var idp = await _dbContext.Product.FindAsync(id);
            if (idp == null)
            {
                return false;
            }
            else
            {
                idp.CategoryId = item.CategoryId;
                idp.BrandId = item.BrandId;
                idp.MaterialId = item.MaterialId;
                idp.ProductName = item.ProductName;
                idp.AvailableQuantity = item.AvailableQuantity;
                idp.Create_At = item.Create_At;
                idp.Update_At = item.Update_At;
                idp.Description = item.Description;
                idp.Long_Description = item.Long_Description;
                idp.Status = item.Status;
                _dbContext.Update(idp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
