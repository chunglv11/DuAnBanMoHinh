using BanMoHinh.Share.Models;

namespace BanMoHinh.API.IServices
{
    public interface ICategoryService
    {
        public Task<bool> Create(Category item);

        public Task<bool> Delete(Guid id);

        public Task<List<Category>> GetAll();

        public Task<Category> GetItem(Guid id);

        public Task<bool> Update(Guid id, Category item);
    }
}
