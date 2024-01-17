using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.API.IServices
{
    public interface IUserService
    {
        public Task<bool> Create(UserViewModel item, string roleName);
        public Task<bool> Lock(string userName);
        public Task<bool> Unlock(string userName);
        public Task<ICollection<User>> GetAll();
        public Task<User> GetItem(string userName);
        public Task<bool> Update(UserViewModel item);
        public Task<bool> ChangeRole( string userName, string roleName);
        public Task<bool> ResetPassword(string userName, string newPassword);
        public Task<bool> ChangePassword(string userName, string currentPassword, string newPassword);
        public  Task<User> GetItemid(Guid userName);
        public Task<bool> CheckPassword(string userName, string currentPassword);


    }
}
