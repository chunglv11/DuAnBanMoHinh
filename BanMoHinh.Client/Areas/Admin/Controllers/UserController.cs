using AspNetCoreHero.ToastNotification.Abstractions;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        public INotyfService _notyf;

        public UserController(HttpClient httpClient, INotyfService notyf)
        {
            _httpClient = httpClient;
            _notyf = notyf;
        }

        public async Task<IActionResult> IndexAsync()
        {
            // show role trong index 
            // nếu là tài khoản của nhân viên thì được phép reset mật khẩu
            
            var user =await _httpClient.GetFromJsonAsync<List<UserViewModel>>("https://localhost:7007/api/users/getall");
            return View(user);
        }
        public async Task<IActionResult> LockUser(string userName)
        {
            
                
                var apiUrl = $"https://localhost:7007/api/users/lock/{userName}";
                var response = await _httpClient.PostAsync(apiUrl, null);

                // Xử lý kết quả từ API
                if (response.IsSuccessStatusCode)
                {
                _notyf.Success("Khoá thành công");
                    return RedirectToAction("Index");
                }
                else
                {
                _notyf.Error("Lỗi");
                    
                    return View();
                }
            
        }
        public async Task<IActionResult> UnlockUser(string userName)
        {


            var apiUrl = $"https://localhost:7007/api/users/unlock/{userName}";
            var response = await _httpClient.PostAsync(apiUrl, null);

            // Xử lý kết quả từ API
            if (response.IsSuccessStatusCode)
            {
                _notyf.Success("Mở thành công");
                return RedirectToAction("Index");
            }
            else
            {
                _notyf.Error("Lỗi");

                return View();
            }

        }
        public async Task<IActionResult> ChangeRole(string userName,string roleName)
        {
            var user = await _httpClient.GetFromJsonAsync<UserViewModel>($"https://localhost:7007/api/users/get/{userName}");
            return View(user);
        }

    }
}
