using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace BanMoHinh.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        HttpClient _client;

        public OrderController(HttpClient httpClient)
        {
            _client = httpClient;
        }
        [HttpGet]
        public async Task<IActionResult> ShowList()
        {
            string apiurl = "https://localhost:7007/api/order/getall";
            var response = await _client.GetAsync(apiurl);
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Order>>(data);
            return View(result);

        }

        [HttpGet]
        public async Task<IActionResult> DetailsOrder(Guid id)
        {

            string apiurl = $"https://localhost:7007/api/order/details/{id}" ;
            var response = await _client.GetAsync(apiurl);
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Order>(data);
            return View(result);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {

           
            string apiurl = "https://localhost:7007/api/order/add";
            var data = JsonConvert.SerializeObject(order);
            StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(apiurl, stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowList");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            string apiurl = $"https://localhost:7007/api/order/details/{id}";
            var response = await _client.GetAsync(apiurl );
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Order>(data);
            return View(result);


        }
        [HttpPost]
        public async Task<IActionResult> Edit(Order order)
        {

            string apiurl = $"https://localhost:7007/api/order/update/{order.Id} ";
            var data = JsonConvert.SerializeObject(order);
            StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(apiurl , stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowList");
            }
            return View();
        }

        public async Task<IActionResult> DeleteC(Guid id )
        {

            string apiurl = $"https://localhost:7007/api/order/delete/{id}";

            var response = await _client.DeleteAsync(apiurl);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowList");
            }
            return View();
        }
        public async Task<IActionResult> DoiTrangThai(Guid idhd, Guid idtrangthai)// Dùng cho trạng thái truyền  vào: 10, 3
        {
            try
            {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

                
                if (identity != null)
                {
                    var userID = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value); // userId

                  
                    var idnv = userID;
                    if (idtrangthai == Guid.Parse("4C54C2DD-2FA5-4041-9B94-FB613BEBDFBC"))
                    {
                        string url = $"order/GiaoThanhCong?idhd={idhd}&idnv={idnv}";
                        var response = await _client.PutAsync(url, null);
                        if (response.IsSuccessStatusCode)
                        {
                            return Json(new { success = true, message = "Cập nhật trạng thái thành công" });
                        }
                    }
                    else
                    {
                        string url = $"order?idhoadon={idhd}&trangthai={idtrangthai}&idnhanvien={idnv}";
                        var response = await _client.PutAsync(url, null);
                        if (response.IsSuccessStatusCode)
                        {
                            return Json(new { success = true, message = "Cập nhật trạng thái thành công" });
                        }
                    }
                }
                return Json(new { success = false, message = "Cập nhật trạng thái thất bại" });
            }
            catch (Exception)
            {
                return RedirectToAction("_QuanLyHoaDon", "QuanLyHoaDon");
            }
        }
        [HttpGet("/QuanLyHoaDon/HuyHD")]
        public async Task<IActionResult> HuyHD(Guid idhd, string ghichu)
        {
            try
            {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

                if (identity != null)
                {
                    var userID = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value); // userId


                    var idnv = userID;
                    if (ghichu != null)
                    {
                        string url = $"HoaDon/HuyHD?idhd={idhd}&idnv={idnv}";
                        var response = await _client.PutAsync(url, null);
                        if (response.IsSuccessStatusCode)
                        {
                            var stringURL = $"https://localhost:7095/api/HoaDon/UpdateGhichu?idhd={idhd}&idnv={idnv}&ghichu={ghichu}";
                            var responseghichu = await _client.PutAsync(stringURL, null);
                            if (responseghichu.IsSuccessStatusCode)
                            {
                                return Json(new { success = true, message = "Cập nhật trạng thái thành công" });
                            }
                        }
                    }
                }
                    return Json(new { success = false, message = "Ghi chú không được để null" });
                
            }
            catch (Exception ex)
            {
                return RedirectToAction("_QuanLyHoaDon", "QuanLyHoaDon");
            }
        }
    }
}
