using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using System.Data.Entity;

namespace BanMoHinh.API.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly MyDbContext _dbContext;

        public CartItemService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddCartItem(CartItem item)
        {
            try
            {
                var cartItem = new CartItem()
                {
                    Id = item.Id,
                    CartId = item.CartId,
                    ProductDetail_ID = item.ProductDetail_ID,
                    Quantity = item.Quantity,
                    Price = item.Price,
                };
                await _dbContext.CartItem.AddAsync(cartItem);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteCartItem(Guid cartItemId, Guid productdetailId, Guid CartId)
        {
            try
            {
                var item = await _dbContext.CartItem.FirstOrDefaultAsync(c => c.Id == cartItemId && c.ProductDetail_ID == productdetailId && c.CartId == CartId);
                _dbContext.CartItem.Remove(item);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> GetCartItemsByCartId(Guid cartItemId)
        {
            try
            {
                var item = await _dbContext.CartItem.FirstOrDefaultAsync(c => c.Id == cartItemId);
                _dbContext.CartItem.Remove(item);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateCartItem(Guid cartItemId, int newquantity, int newPrice)
        {
            try
            {
                if (cartItemId != null) {
                    var item = await _dbContext.CartItem.FirstOrDefaultAsync(c => c.Id == cartItemId);
                    item.Quantity = newquantity;
                    item.Price = newPrice;
                    _dbContext.CartItem.Update(item);
                    await _dbContext.SaveChangesAsync();
                }
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
