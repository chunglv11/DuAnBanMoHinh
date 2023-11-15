using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BanMoHinh.API.Services
{
    public class WishListService : IWishListService
    {
        private MyDbContext _dbContext;
        private IUserService _userService;
        private IProductService _productService;

        public WishListService(MyDbContext dbContext, IUserService userService, IProductService productService)
        {
            _dbContext = dbContext;
            _userService = userService;
            _productService = productService;
        }

        public async Task<bool> Create(Guid UserId, Guid ProductId)
        {
            try
            {
                //var idp = _dbContext.Product.FirstOrDefault(c => c.Id == ProductId).Id;
                //var iduser = _dbContext.Users.FirstOrDefault(c => c.Id == UserId).Id;
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

        public async Task<List<WishListVM>> GetAll()
        {
            var result = (from pd in _dbContext.WishList
                          join p in _dbContext.Product on pd.ProductId equals p.Id
                          join u in _dbContext.Users on pd.UserId equals u.Id
                          select new WishListVM()
                          {
                              Id = pd.Id,
                              UserId = pd.UserId,
                              ProductId = pd.ProductId,
                              ProductName = p.ProductName
                          }).ToList();

            return result;
        }

        public async Task<WishList> GetItem(Guid id)
        {
            return await _dbContext.WishList.FindAsync(id);
        }

    }
}
