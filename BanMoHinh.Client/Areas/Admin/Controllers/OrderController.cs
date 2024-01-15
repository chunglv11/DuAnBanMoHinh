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
            var user = await _client.GetFromJsonAsync<List<UserViewModel>>("https://localhost:7007/api/users/getall");
            ViewData["User"] = user;
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
        public async Task<IActionResult> DoiTrangThai(Guid idhd, int trangthai)// Dùng cho trạng thái truyền  vào: 10, 3
        {

            try
            {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

                
                if (identity != null)
                {
                    var userID = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value); // userId


                    var idnv = userID;
                   
                     if(trangthai == 2) // chờ lấy hàng
                    {
                        Guid idtt = Guid.Parse("2C54C2DD-2FA5-4041-9B94-FB613BEBDFBC");
                        string url = $"https://localhost:7007/api/order/updatett?idhoadon={idhd}&trangthai={idtt}&idnhanvien={idnv}";
                        var response = await _client.PutAsync(url, null);
                        if (response.IsSuccessStatusCode)
                        {
                            TempData["SuccessMessage"] = "Cập nhật trạng thái thành công!";
                            var successMessage = TempData["SuccessMessage"] as string;
                            if (!string.IsNullOrEmpty(successMessage))
                            {
                                ViewData["SuccessMessage"] = successMessage;
                                ViewData["ShowSuccessMessage"] = true;
                            }
                            return RedirectToAction("ShowList");

                        }
                    }
                    else if (trangthai == 3)// dang giao hàng
                    {
                        Guid idtt = Guid.Parse("3C54C2DD-2FA5-4041-9B94-FB613BEBDFBC");
                        string url = $"https://localhost:7007/api/order/updatett?idhoadon={idhd}&trangthai={idtt}&idnhanvien={idnv}";
                        var response = await _client.PutAsync(url, null);
                        if (response.IsSuccessStatusCode)
                        {
                            ViewBag.SuccessMessage = "Cập nhật trạng thái thành công";
                            return RedirectToAction("ShowList");

                        }
                    }
                    else if (trangthai == 4)//giao hàng thành công
                    {
                        string url = $"https://localhost:7007/api/order/GiaoThanhCong?idhd={idhd}&idnv={idnv}";
                        var response = await _client.PutAsync(url, null);
                        if (response.IsSuccessStatusCode)
                        {
                            ViewBag.SuccessMessage = "Cập nhật trạng thái thành công";
                            return RedirectToAction("ShowList");

                        }
                    }
                    else if (trangthai == 5)//giao hàng không thành công
                    {
                        Guid idtt = Guid.Parse("5C54C2DD-2FA5-4041-9B94-FB613BEBDFBC");
                        string url = $"https://localhost:7007/api/order/updatett?idhoadon={idhd}&trangthai={idtt}&idnhanvien={idnv}";
                        var response = await _client.PutAsync(url, null);
                        if (response.IsSuccessStatusCode)
                        {
                            ViewBag.SuccessMessage = "Cập nhật trạng thái thành công";
                            return RedirectToAction("ShowList");

                        }
                    }
                   
                }
                ViewBag.ErrorMessage = "Cập nhật trạng thái thất bại";
                return RedirectToAction("ShowList");

            }
            catch (Exception)
            {
                return RedirectToAction("ShowList", "Order");
            }
        }
        [HttpGet("/Order/HuyHD")]
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
                        string url = $"https://localhost:7007/api/order/HuyHD?idhd={idhd}&idnv={idnv}";
                        var response = await _client.PutAsync(url, null);
                        if (response.IsSuccessStatusCode)
                        {
                            var stringURL = $"https://localhost:7007/api/order/UpdateGhichu?idhd={idhd}&idnv={idnv}&ghichu={ghichu}";
                            var responseghichu = await _client.PutAsync(stringURL, null);
                            if (responseghichu.IsSuccessStatusCode)
                            {
                                ViewBag.SuccessMessage = "Cập nhật trạng thái thành công";
                                return RedirectToAction("ShowList");


                            }
                        }
                    }
                    ViewBag.ErrorMessage = "Ghi chú không được trống";

                }
                return RedirectToAction("ShowList");



            }
            catch (Exception ex)
            {
                return RedirectToAction("ShowList", "Order");

            }
        }
        [HttpGet("/Order/ViewChiTietHD/{idhd}")]
        public async Task<IActionResult> ViewChiTietHD(string idhd)
        {
            var hd = await _client.GetFromJsonAsync<DonMuaChiTietVM>($"https://localhost:7007/api/order/GetAllDonMuaChiTiet1/{idhd}");
            if (hd == null)
            {
                return NotFound();
            }
            return PartialView("ChiTietHD", hd);
        }
    }
}
