using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.API.IServices
{
    public interface IUserService
    {
        public Task<bool> Create(UserViewModel item, string roleName);
        public Task<bool> Delete(Guid id);
        public Task<List<User>> GetAll();
        public Task<User> GetItem(Guid id);
        public Task<bool> Update(Guid id, User item);
        public Task<bool> ChangeRole(Guid userId, string roleName);
    }
}
