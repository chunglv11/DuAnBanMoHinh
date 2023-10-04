using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.API.IServices
{
    public interface IBrandService
    {
        public Task<bool> Create(BrandVM item);

        public Task<bool> Delete(Guid id);

        public Task<List<Brand>> GetAll();

        public Task<Brand> GetItem(Guid id);

        public Task<bool> Update(Guid id, BrandVM item);
    }
}
