using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.API.IServices
{
    public interface IProductImageService
    {
        public Task<bool> Create(ProductImageVM item);

        public Task<bool> CreateMany(List<ProductImageVM> items);

        public Task<bool> Delete(Guid id);

        //public Task<bool> DeleteMany(List<> items);

        public Task<IEnumerable<ProductImage>> GetAll();

        public Task<ProductImage> GetItem(Guid id);

        public Task<bool> Update(Guid id, ProductImageVM item);
    }
}
