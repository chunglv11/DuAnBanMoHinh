using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.API.IServices
{
    public interface IPostService
    {
        public Task<bool> Create(PostVM item);

        public Task<bool> Delete(Guid id, Guid UserId);

        public Task<List<Post>> GetAll();

        public Task<Post> GetItem(Guid id);

        public Task<bool> Update(Guid id,Guid UserId, PostVM item);
    }
}
