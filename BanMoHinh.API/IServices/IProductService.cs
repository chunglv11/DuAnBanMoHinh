using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.API.IServices
{
    public interface IProductService
    {
        public Task<bool> Create(ProductVM item);

        public Task<bool> CreateMany(List<ProductVM> items);

        public Task<bool> Delete(Guid id);

        //public Task<bool> DeleteMany(List<> items);

        public Task<IEnumerable<Product>> GetAll();
        public Task<IEnumerable<ProductVM>> GetAllVM();

        public Task<Product> GetItem(Guid id);

        public Task<bool> Update(Guid id, ProductVM item);
    }
}
