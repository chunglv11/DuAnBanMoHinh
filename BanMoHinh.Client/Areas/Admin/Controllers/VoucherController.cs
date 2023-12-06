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
			var voucherStatus = await _httpClient.GetFromJsonAsync<List<VoucherStatus>>("https://localhost:7007/api/VoucherStatus");
			var vouchertype = await _httpClient.GetFromJsonAsync<List<VoucherType>>("https://localhost:7007/api/VoucherType");

			ViewData["vouchertype"] = vouchertype;
			ViewData["voucherstatus"] = voucherStatus;

			return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateVoucher(Voucher voucher)
        {
      
            voucher.Status = true;
            var creatvoucher = await _httpClient.PostAsJsonAsync("https://localhost:7007/api/voucher/create-voucher", voucher);
            if (creatvoucher.IsSuccessStatusCode)
            {
                return RedirectToAction("GetallVoucher");
            }
            return View();
        }

        public async Task<IActionResult> detailVoucher(Guid id)
        {
            var voucherStatus = await _httpClient.GetFromJsonAsync<List<VoucherStatus>>("https://localhost:7007/api/VoucherStatus");
            var vouchertype = await _httpClient.GetFromJsonAsync<List<VoucherType>>("https://localhost:7007/api/VoucherType");

            ViewData["vouchertype"] = vouchertype;
            ViewData["voucherstatus"] = voucherStatus;

            var result = await _httpClient.GetFromJsonAsync<Voucher>($"https://localhost:7007/api/voucher/get-{id}");

            return View(result);
        }
        public async Task<IActionResult> editVoucher(Guid id)
		{
			var voucherStatus = await _httpClient.GetFromJsonAsync<List<VoucherStatus>>("https://localhost:7007/api/VoucherStatus");
			var vouchertype = await _httpClient.GetFromJsonAsync<List<VoucherType>>("https://localhost:7007/api/VoucherType");

			ViewData["vouchertype"] = vouchertype;
			ViewData["voucherstatus"] = voucherStatus;

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
