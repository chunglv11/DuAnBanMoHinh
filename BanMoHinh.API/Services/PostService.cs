using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace BanMoHinh.API.Services
{
    public class PostService : IPostService
    {
        private readonly MyDbContext _dbContext;

        public PostService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Create(Post item)
        {
            try
            {
                var post = new Post()
                {
                    UserId = item.UserId,
                    Tittle = item.Tittle,
                    TittleImage = item.TittleImage,
                    Contents = item.Contents,
                    CreateAt = item.CreateAt,
                    UpdateAt = item.UpdateAt,
                    Description = item.Description,
                    Status = item.Status
                };
                await _dbContext.Posts.AddAsync(post);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> Delete(Guid id, Guid UserId)
        {
            try
            {
                    var item = await _dbContext.Posts.FirstOrDefaultAsync(c=>c.Id==id&&c.UserId==UserId);
                    _dbContext.Posts.Remove(item);
                    await _dbContext.SaveChangesAsync();
                    return true;

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<List<Post>> GetAll()
        {
            return await _dbContext.Posts.ToListAsync();
        }

        public async Task<Post> GetItem(Guid id)
        {
            return await _dbContext.Posts.FindAsync(id);
        }

        public async Task<bool> Update(Guid id, Guid UserId, Post item)
        {
            try
            {
                var postForcus = await _dbContext.Posts.FirstOrDefaultAsync(c => c.Id == id && c.UserId == UserId);
                postForcus.Tittle = item.Tittle;
                postForcus.TittleImage = item.TittleImage;
                postForcus.Contents = item.Contents;
                postForcus.CreateAt = item.CreateAt;
                postForcus.UpdateAt = item.UpdateAt;
                postForcus.Description = item.Description;
                postForcus.Status = item.Status;
                 _dbContext.Posts.Update(postForcus);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
