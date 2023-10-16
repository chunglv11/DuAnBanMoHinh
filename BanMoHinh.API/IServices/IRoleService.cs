using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.API.IServices
{
    public interface IRoleService
    {
        public Task<bool> Create(Role roleName);
        public Task<bool> Delete(string roleName);
        public Task<ICollection<Role>> GetAll();
        public Task<Role> GetItem(string roleName);
        public Task<bool> Update(Role role);
    }
}
