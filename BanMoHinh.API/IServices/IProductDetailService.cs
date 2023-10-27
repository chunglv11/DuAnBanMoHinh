using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.API.IServices
{
    public interface IProductDetailService
    {
        public Task<bool> Create(ProductDetailVM item, IFormFileCollection filecollection);

        public Task<bool> CreateMany(List<ProductDetailVM> items);

        public Task<bool> Delete(Guid id);

        //public Task<bool> DeleteMany(List<> items);

        public Task<IEnumerable<ProductDetailVM>> GetAll();

        public Task<ProductDetail> GetItem(Guid id);

        public Task<bool> Update(ProductDetailVM item);
    }
}
