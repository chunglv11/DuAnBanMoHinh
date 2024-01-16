using BanMoHinh.Share.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BanMoHinh.Client.Controllers
{
    public class VoucherController : Controller
    {
        private readonly HttpClient _httpClient;

        public VoucherController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> GetVoucherForU()
        {
            try
            {
                // Lấy thông tin người dùng đang đăng nhập từ HttpContext
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!string.IsNullOrEmpty(userId))
                {
                    var allvoucherU = await _httpClient.GetFromJsonAsync<List<UserVoucher>>("https://localhost:7007/api/UserVoucher/get-uservoucher");
                    // Lọc danh sách UserVoucher của người dùng đăng nhập
                    var userVouchers = allvoucherU.Where(uv => uv.UserId == Guid.Parse(userId) && uv.Status == true).ToList();
                    // Lấy danh sách voucherIds từ userVouchers
                    var voucherIds = userVouchers.Select(uv => uv.VoucherId).ToList();
                    // Lấy danh sách Voucher tương ứng
                    var allVouchers = await _httpClient.GetFromJsonAsync<List<Voucher>>("https://localhost:7007/api/voucher/get-vouchers");
                    var userVouchersWithVouchers = allVouchers.Where(v => voucherIds.Contains(v.Id)).ToList();
                    return View(userVouchersWithVouchers);
                }
                else
                {
                    // Người dùng chưa đăng nhập, xử lý theo yêu cầu của bạn
                    return RedirectToAction("Login"); // hoặc thực hiện hành động phù hợp
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                Console.WriteLine(ex.Message);
                return View(); // hoặc RedirectToAction("Error");
            }
        }
    }
}
