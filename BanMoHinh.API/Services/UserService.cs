using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BanMoHinh.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IRankService _rankService;
        private readonly RoleManager<Role> _roleManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager,RoleManager<Role> roleManager, IConfiguration configuration,IRankService rankService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _rankService = rankService;
            _roleManager = roleManager;
        }
        // CREATE
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
        // DELETE
        public async Task<bool> Lock(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                // Đặt thời gian kết thúc lockout là vô hạn
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);

                return true;
            }
            return false;
        }

        public async Task<bool> Unlock(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                // Mở khóa người dùng bằng cách đặt thời gian kết thúc lockout thành null
                await _userManager.SetLockoutEndDateAsync(user, null);

                return true;
            }
            return false;
        }


        // GET ALL USER
        public async Task<ICollection<User>> GetAll()
        {
            // Lấy tất cả người dùng từ UserManager
            var allUsers = await _userManager.Users.ToListAsync();

            // Lọc những người dùng không thuộc role "admin"
            var nonAdminUsers = allUsers.Where(user => !_userManager.IsInRoleAsync(user, "Admin").Result).ToList();

            return nonAdminUsers;
        }

        // GET USER
        public async Task<User> GetItem(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
        // RESET PASSWORD
        public async Task<bool> ResetPassword(string userName, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var result = await _userManager.RemovePasswordAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddPasswordAsync(user, newPassword);
                    if (result.Succeeded)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        // CHANGE PASSWORD
        public async Task<bool> ChangePassword(string userName, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);
            bool passwordMatch = await _userManager.CheckPasswordAsync(user, currentPassword); // check old password
            if (passwordMatch != null)
            {
                var results = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword); // change password
                if (results.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }
        // CHANGE ROLE
        public async Task<bool> ChangeRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                
                var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(user, roleName);
                    if (result.Succeeded)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // UPDATE
        public async Task<bool> Update( UserViewModel item) // tự nhiên có rank khoai ghê
        {
            var user = new User()
            {
                UserName = item.UserName,
                Email = item.Email,
                PhoneNumber = item.PhoneNumber,
                DateOfBirth = item.DateOfBirth,
                Points = 0,
            };
            var result = await _userManager.UpdateAsync(user); // update account
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
