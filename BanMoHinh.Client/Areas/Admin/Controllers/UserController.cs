using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> IndexAsync()
        {
            // show role trong index 
            // nếu là tài khoản của nhân viên thì được phép reset mật khẩu
            var user =await _httpClient.GetFromJsonAsync<List<UserViewModel>>("https://localhost:7007/api/users");
            return View(user);
        }
        public async Task<IActionResult> DeleteUser(string userName)
        {
            await _httpClient.DeleteAsync($"https://localhost:7007/api/users/delete/{userName}");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ChangeRole(string userName,string roleName)
        {
            var user = await _httpClient.GetFromJsonAsync<UserViewModel>($"https://localhost:7007/api/users/get/{userName}");
            return View(user);
        }

    }
}
