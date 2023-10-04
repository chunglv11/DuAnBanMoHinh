using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.API.IServices
{
    public interface IMaterialService
    {
        public Task<bool> Create(MaterialVM item);

        public Task<bool> Delete(Guid id);

        public Task<List<Material>> GetAll();

        public Task<Material> GetItem(Guid id);

        public Task<bool> Update(Guid id, MaterialVM item);
    }
}
