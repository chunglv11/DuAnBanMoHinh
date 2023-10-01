using BanMoHinh.Share.Models;

namespace BanMoHinh.API.IServices
{
    public interface IUserService
    {
        public Task<bool> Create(User item);
        public Task<bool> Delete(Guid id);
        public Task<List<User>> GetAll();
        public Task<User> GetItem(Guid id);
        public Task<bool> Update(Guid id, User item);
    }
}
