using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using Microsoft.AspNetCore.Identity;

namespace BanMoHinh.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public Task<bool> Create(User item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Guid id, User item)
        {
            throw new NotImplementedException();
        }
    }
}
