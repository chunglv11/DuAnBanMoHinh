using BanMoHinh.Share.Models;

namespace BanMoHinh.API.IServices
{
    public interface IWishListService
    {
        public Task<bool> Create(Guid UserId, Guid ProductDetailId);

        public Task<bool> Delete(Guid Id);

        public Task<List<WishList>> GetAll();

        public Task<WishList> GetItem(Guid Id);

    }
}
