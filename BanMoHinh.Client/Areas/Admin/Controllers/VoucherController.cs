using BanMoHinh.Share.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;

namespace BanMoHinh.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VoucherController : Controller
    {
       

        private readonly HttpClient _httpClient;


        public VoucherController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> GetallVoucher()    
        {
            var allvoucher = await _httpClient.GetFromJsonAsync<List<Voucher>>("https://localhost:7007/api/voucher/get-vouchers");
            return View(allvoucher);
        }
        public async Task<IActionResult> CreateVoucher()
		{
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateVoucher(Voucher voucher)
        {
			//var allrank = await _httpClient.GetFromJsonAsync<List<Rank>>("https://localhost:7007/api/ranks/get-ranks");

			
            voucher.Id = Guid.NewGuid();
			voucher.Create_Date = DateTime.Now;
            //var allvoucher = await _httpClient.GetFromJsonAsync<List<Voucher>>("https://localhost:7007/api/voucher/get-vouchers");
            //if (voucher.Minimum_order_value != null || voucher.Code != null || voucher.Value != null || voucher.Discount_Type != null  || voucher.Start_Date != null || voucher.End_Date != null)
            //{
                //if (voucher.Minimum_order_value < 0)
                //{
                //    ViewData["SoTienCan"] = "Số tiền cần không được âm ";
                //}
                //if (voucher.Value <= 0)
                //{
                //    ViewData["GiaTri"] = "Mời bạn nhập giá trị lớn hơn 0";
                //}
                //if (voucher.Quantity <= 0)
                //{
                //    ViewData["SoLuong"] = "Mời bạn nhập số lượng lớn hơn 0";
                //}
                //if (voucher.End_Date < voucher.Start_Date)
                //{
                //    ViewData["Ngay"] = "Ngày kết thúc phải lớn hơn ngày áp dụng";
                //}
                //var timkiem = allvoucher.FirstOrDefault(x => x.Code == voucher.Code.Trim());
                //if (timkiem != null)
                //{
                //    ViewData["Ma"] = "Mã này đã tồn tại";
                //}
				var response = await _httpClient.PostAsJsonAsync($"https://localhost:7007/api/voucher/create-voucher", voucher);
                if (response.IsSuccessStatusCode)
                {
					var alluser = await _httpClient.GetFromJsonAsync<List<User>>("https://localhost:7007/api/users/getall");
					//add voucher sở hữu
					foreach (var user in alluser)
					{
						UserVoucher uv = new UserVoucher();
						uv.Id = Guid.NewGuid();
						uv.VoucherId = voucher.Id;
						uv.UserId = user.Id;
                        uv.Status = true;
						var createUV = await _httpClient.PostAsJsonAsync("https://localhost:7007/api/UserVoucher/create-uservoucher", uv);
                        if (!createUV.IsSuccessStatusCode)
                        {
                            return BadRequest();
                        }
					}
                    
					return RedirectToAction("GetallVoucher");
				}
                //lấy tất cả user
				
			//}
            return View();
        }

        public async Task<IActionResult> detailVoucher(Guid id)
        {


            var result = await _httpClient.GetFromJsonAsync<Voucher>($"https://localhost:7007/api/voucher/get-{id}");

            return View(result);
        }
        public async Task<IActionResult> editVoucher(Guid id)
		{

            var result = await _httpClient.GetFromJsonAsync<Voucher>($"https://localhost:7007/api/voucher/get-{id}");

			return View(result);
		}
		[HttpPost]
		public async Task<IActionResult> editVoucher(Voucher voucher)
		{
			var result = await _httpClient.PutAsJsonAsync($"https://localhost:7007/api/voucher/update-voucher-{voucher.Id}", voucher);
			return RedirectToAction("GetallVoucher");
		}

		public async Task<IActionResult> DeleteVoucher(Guid id)
		{
			await _httpClient.DeleteAsync($"https://localhost:7007/api/voucher/delete-voucher-{id}");
			return RedirectToAction("GetallVoucher");
		}
        public async Task<IActionResult> CreatevoucherProduct()
        {
            var allProduct = await _httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7007/api/product/get-all-product");
            return View(allProduct);
        }
    }
     

}
