using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity;

namespace BanMoHinh.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IRankService _rankService;
        private readonly MyDbContext _myDbContext;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration,IRankService rankService,MyDbContext myDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _rankService = rankService;
            _myDbContext = myDbContext;
        }

        public Task<bool> ChangeRole(Guid userId, string roleName)
        {
            throw new NotImplementedException();
        }



        public async Task<bool> Create(UserViewModel item, string roleName)
        {
            var newRank = await _rankService.GetItemByName("Bạc");
            var user = new User()
            {
                UserName = item.UserName,
                Email = item.Email,
                PhoneNumber = item.PhoneNumber,
                DateOfBirth = item.DateOfBirth,
                RankId = newRank.Id,
                Points = 0,
            };
            var result = await _userManager.CreateAsync(user, item.PassWord); // add account
            if (result.Succeeded)
            {
                // add to role
                await _userManager.AddToRoleAsync(user, "User");
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                return true;
            }
            return false;
        }

        public async Task<List<User>> GetAll()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User> GetItem(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public Task<bool> Update(Guid id, User item)
        {
            throw new NotImplementedException();
        }
    }
}
