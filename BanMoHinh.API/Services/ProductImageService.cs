using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BanMoHinh.API.Services
{
    public class ProductImageService : IProductImageService
    {
        private MyDbContext _dbContext;

        public ProductImageService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(ProductImageVM item)
        {
            try
            {
                ProductImage productImage = new ProductImage()
                {
                    Id = Guid.NewGuid(),
                    ProductDetailId = item.ProductDetailId,
                    ImageUrl = item.ImageUrl,
                };
                await _dbContext.ProductImage.AddAsync(productImage);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> CreateMany(List<ProductImageVM> items)
        {
            try
            {
                var lstPi = new List<ProductImage>();
                foreach (var x in items)
                {
                    ProductImage productImage = new ProductImage()
                    {
                        Id = Guid.NewGuid(),
                        ProductDetailId = x.ProductDetailId,
                        ImageUrl = x.ImageUrl,
                    };
                    lstPi.Add(productImage);

                }
                await _dbContext.ProductImage.AddRangeAsync(lstPi);
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
                var idp = await _dbContext.ProductImage.FindAsync(id);
                _dbContext.Remove(idp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductImage>> GetAll()
        {
            return await _dbContext.ProductImage.ToListAsync();
        }

        public async Task<ProductImage> GetItem(Guid id)
        {
            return await _dbContext.ProductImage.FindAsync(id);
        }

        public async Task<bool> Update(Guid id, ProductImageVM item)
        {
            var idp = await _dbContext.ProductImage.FindAsync(id);
            if (idp == null)
            {
                return false;
            }
            else
            {
                idp.ProductDetailId = item.ProductDetailId;
                idp.ImageUrl = item.ImageUrl;
                _dbContext.Update(idp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
