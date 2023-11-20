using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.API.IServices
{
    public interface IWishListService
    {
        public Task<bool> Create(Guid UserId, Guid ProductId);

        public Task<bool> Delete(Guid Id);

        public Task<List<WishListVM>> GetAll();

        public Task<WishList> GetItem(Guid Id);

    }
}
