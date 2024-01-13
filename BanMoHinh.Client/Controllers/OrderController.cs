﻿using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;
using X.PagedList;

namespace BanMoHinh.Client.Controllers
{
	public class OrderController : Controller
	{
		private readonly HttpClient _httpClient;

		public OrderController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
        public async Task<IActionResult> allOrder(Guid id, int? page)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userIdClaim = identity.FindFirst(ClaimTypes.Name);
            if (page == null) page = 1;
            var userName = userIdClaim.Value;
            var getUserbyName = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7007/api/users/get/{userName}");
            var listOrder = await _httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7007/api/order/getall");
            var listorderbyUser = listOrder.Where(c => c.UserId == getUserbyName.Id);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(listorderbyUser.ToPagedList(pageNumber, pageSize));

        }
        public IActionResult OrderDetails(Guid idHoaDon)
        {
            
                List<DonMuaChiTietVM> DonMuaCT = new List<DonMuaChiTietVM>();
                HttpResponseMessage responseDonMuaCT =  _httpClient.GetAsync($"https://localhost:7007/api/order/GetAllDonMuaChiTiet?idHoaDon={idHoaDon}").Result;
                if (responseDonMuaCT.IsSuccessStatusCode)
                {
                    DonMuaCT = JsonConvert.DeserializeObject<List<DonMuaChiTietVM>>(responseDonMuaCT.Content.ReadAsStringAsync().Result);
                }
                return View("OrderDetails", DonMuaCT);
            
        }
        public async Task<IActionResult> HuyDon(Guid idHoaDon, Guid? idTrangThai)
        {
            var listOrder = await _httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7007/api/order/getall");
            
            var firstOrder = listOrder.FirstOrDefault();
            if (firstOrder != null)
            {
                idTrangThai = firstOrder.OrderStatusId = Guid.Parse("6C54C2DD-2FA5-4041-9B94-FB613BEBDFBC");//huỷ đơn
            }
            //update trạng thái hoá đơn
            HttpResponseMessage responseDonMua = await _httpClient.GetAsync($"https://localhost:7007/api/order/updatestatus?OrderId={idHoaDon}&StatusId={idTrangThai}");
            
            if (responseDonMua.IsSuccessStatusCode)
            {
                //hoàn lại voucher
                if (firstOrder.VoucherId != null)
                {
                    // + số lượng voucher 

                    var updateSL = await _httpClient.GetAsync($"https://localhost:7007/api/voucher/TangGiamSoLuongTheoId?voucherId={firstOrder.VoucherId}&tanggiam=true");

                    //  sửa trạng thái trong uservoucher  

                    var updateStatus = await _httpClient.GetAsync($" https://localhost:7007/api/UserVoucher/updatetrangthai?voucherId={firstOrder.VoucherId}&userId={firstOrder.UserId}&status=true");

                }
                //hoàn lại số lượng
                var listOrderItem = await _httpClient.GetFromJsonAsync<List<OrderItem>>("https://localhost:7007/api/orderitem/getall");
                var productDetail = await _httpClient.GetFromJsonAsync<List<ProductDetailVM>>("https://localhost:7007/api/productDetail/get-all-productdetail");
                var lsthdct = listOrderItem.Where(c => c.OrderId == idHoaDon).ToList();
                foreach (var hdct in lsthdct)
                {
                    var ctsp = productDetail.FirstOrDefault(c => c.Id == hdct.ProductDetailId);
                    ctsp.Quantity += hdct.Quantity;
                    var result = await _httpClient.PutAsJsonAsync($"https://localhost:7007/api/productDetail/update-productdetail-{ctsp.Id}", ctsp);
                }
                return RedirectToAction("allOrder");
            }

            return RedirectToAction("allOrder");
        }




    }
}
