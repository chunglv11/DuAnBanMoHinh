using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using Microsoft.EntityFrameworkCore;

namespace BanMoHinh.API.Services
{
    public class WishListService : IWishListService
    {
        private MyDbContext _dbContext;


        public WishListService(MyDbContext myDbContext)
        {
            _dbContext = myDbContext;
        }

        public async Task<bool> Create(Guid UserId, Guid ProductId)
        {
            try
            {
                WishList wishList = new WishList()
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    ProductId = ProductId
                };
                await _dbContext.WishList.AddAsync(wishList);
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
                var item = await _dbContext.WishList.FirstOrDefaultAsync(c => c.Id == id);
                _dbContext.WishList.Remove(item);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<List<WishList>> GetAll()
        {
            return await _dbContext.WishList.ToListAsync();
        }

        public async Task<WishList> GetItem(Guid id)
        {
            return await _dbContext.WishList.FindAsync(id);
        }

    }
}
