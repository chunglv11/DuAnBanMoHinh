using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BanMoHinh.API.Services
{
    public class ProductDetailService : IProductDetailService
    {
        private MyDbContext _dbContext;

        public ProductDetailService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(ProductDetailVM item)
        {
            try
            {
                ProductDetail productDetail = new ProductDetail()
                {
                    Id = Guid.NewGuid(),
                    ProductId = item.ProductId,
                    SizeId = item.SizeId,
                    ColorId = item.ColorId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    PriceSale = item.PriceSale,
                    Create_At = item.Create_At,
                    Update_At = item.Update_At,
                    Description = item.Description,
                    Status = item.Status,
                };
                await _dbContext.ProductDetail.AddAsync(productDetail);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> CreateMany(List<ProductDetailVM> items)
        {
            try
            {
                var LstProductDetail = new List<ProductDetail>();
                foreach (var x in items)
                {
                    ProductDetail productDetail = new ProductDetail()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = x.ProductId,
                        SizeId = x.SizeId,
                        ColorId = x.ColorId,
                        Quantity = x.Quantity,
                        Price = x.Price,
                        PriceSale = x.PriceSale,
                        Create_At = x.Create_At,
                        Update_At = x.Update_At,
                        Description = x.Description,
                        Status = x.Status,
                    };
                    LstProductDetail.Add(productDetail);
                }
                await _dbContext.ProductDetail.AddRangeAsync(LstProductDetail);
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
                var idp = await _dbContext.ProductDetail.FindAsync(id);
                _dbContext.Remove(idp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductDetail>> GetAll()
        {
            return await _dbContext.ProductDetail.ToListAsync();
        }

        public async Task<ProductDetail> GetItem(Guid id)
        {
            return await _dbContext.ProductDetail.FindAsync(id);
        }

        public async Task<bool> Update(Guid id, ProductDetailVM item)
        {
            var idp = await _dbContext.ProductDetail.FindAsync(id);
            if (idp == null)
            {
                return false;
            }
            else
            {
                idp.SizeId = item.SizeId;
                idp.ColorId = item.ColorId;
                idp.ProductId = item.ProductId;
                idp.Quantity = item.Quantity;
                idp.Price = item.Price;
                idp.PriceSale = item.PriceSale;
                idp.Create_At = item.Create_At;
                idp.Update_At = item.Update_At;
                idp.Description = item.Description;
                idp.Status = item.Status;
                _dbContext.ProductDetail.Update(idp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
