using AppData.Models;
using AppData.ViewModels;
using AppData.ViewModels.ThongKe;
using AppView.PhanTrang;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace AppView.Controllers
{
    public class VouchersController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly BanMoHinhDBContext dBContext;
        public VouchersController()
        {
            _httpClient = new HttpClient();
            dBContext=new BanMoHinhDBContext();
        }
        public int PageSize = 8;
        // get all vocher
        [HttpGet]
        public async Task<IActionResult> GetAllVoucher(int ProductPage = 1)
        {
            string apiURL = $"https://localhost:7095/api/Voucher";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var roles = JsonConvert.DeserializeObject<List<VoucherView>>(apiData);
            return View(new PhanTrangVouchers
            {
                listvouchers = roles
                        .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = ProductPage,
                    TotalItems = roles.Count()
                }

            }
                );

        }
        // tim kiem ten
        [HttpGet]
        public async Task<IActionResult> TimKiemTenVC(string Ten, int ProductPage = 1)
        {
            string apiURL = $"https://localhost:7095/api/Voucher";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var roles = JsonConvert.DeserializeObject<List<VoucherView>>(apiData);


            return View("GetAllVoucher", new PhanTrangVouchers
            {
                listvouchers = roles.Where(x => x.Ten.Contains(Ten.Trim()))
                        .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = ProductPage,
                    TotalItems = roles.Count()
                }

            }
                );

        }
        // create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(VoucherView voucher)
        {
            try
            {
                string apiURL = $"https://localhost:7095/api/Voucher";
                var response1 = await _httpClient.GetAsync(apiURL);
                var apiData = await response1.Content.ReadAsStringAsync();
                var roles = JsonConvert.DeserializeObject<List<VoucherView>>(apiData);

                if (voucher.SoTienCan != null || voucher.Ten != null || voucher.GiaTri != null || voucher.HinhThucGiamGia != null || voucher.TrangThai != null || voucher.NgayApDung != null || voucher.NgayKetThuc != null)
                {
                    if (voucher.SoTienCan < 0)
                    {
                        ViewData["SoTienCan"] = "Số tiền cần không được âm ";
                    }
                    if (voucher.GiaTri <= 0)
                    {
                        ViewData["GiaTri"] = "Mời bạn nhập giá trị lớn hơn 0";
                    }
                    if (voucher.SoLuong <= 0)
                    {
                        ViewData["SoLuong"] = "Mời bạn nhập số lượng lớn hơn 0";
                    }
                    if (voucher.NgayKetThuc < voucher.NgayApDung)
                    {
                        ViewData["Ngay"] = "Ngày kết thúc phải lớn hơn ngày áp dụng";
                    }
                    var timkiem = roles.FirstOrDefault(x => x.Ten == voucher.Ten.Trim());
                    if (timkiem != null)
                    {
                        ViewData["Ma"] = "Mã này đã tồn tại";
                        
                    }

                    if (voucher.HinhThucGiamGia == 1)
                    {
                        if (voucher.SoTienCan == 0)
                        {
                            if (voucher.GiaTri > 100 || voucher.GiaTri <= 0)
                            {
                                ViewData["GiaTri"] = "Giá trị từ 1 đến 100";
                                return View();
                            }
                            if (voucher.GiaTri <= 100 && voucher.GiaTri > 0)
                            {
                                if (voucher.SoTienCan >= 0 && voucher.GiaTri > 0 && voucher.SoLuong > 0 && voucher.NgayKetThuc >= voucher.NgayApDung && timkiem == null)
                                {
                                    var response = await _httpClient.PostAsJsonAsync($"https://localhost:7095/api/Voucher", voucher);
                                    if (response.IsSuccessStatusCode)
                                    {
                                        return RedirectToAction("GetAllVoucher");
                                    }
                                    return View();
                                }
                            }
                        }
                        if (voucher.SoTienCan > 0)
                        {
                            if (voucher.GiaTri <= voucher.SoTienCan)
                            {
                                if (voucher.GiaTri <= 100 && voucher.GiaTri > 0)
                                {
                                    if (voucher.SoTienCan >= 0 && voucher.GiaTri > 0 && voucher.SoLuong > 0 && voucher.NgayKetThuc >= voucher.NgayApDung && timkiem == null)
                                    {
                                        var response = await _httpClient.PostAsJsonAsync($"https://localhost:7095/api/Voucher", voucher);
                                        if (response.IsSuccessStatusCode)
                                        {
                                            return RedirectToAction("GetAllVoucher");
                                        }
                                        return View();
                                    }
                                }
                                if (voucher.GiaTri > 100 || voucher.GiaTri <= 0)
                                {
                                    ViewData["GiaTri"] = "Giá trị từ 1 đến 100";
                                    return View();
                                }

                            }
                            if (voucher.GiaTri > voucher.SoTienCan)
                            {
                                ViewData["GiaTri"] = "Giá trị phải nhỏ hơn hoặc bằng số tiền cần";
                                return View();

                            }
                        }


                    }
                    if (voucher.HinhThucGiamGia == 0)
                    {
                        if (voucher.SoTienCan == 0)
                        {
                            if (voucher.GiaTri <= 0)
                            {
                                ViewData["GiaTri"] = "Giá trị phải lớn hơn 0";
                                return View();
                            }
                            if (voucher.GiaTri > 0)
                            {
                                if (voucher.SoTienCan >= 0 && voucher.GiaTri > 0 && voucher.SoLuong > 0 && voucher.NgayKetThuc >= voucher.NgayApDung && timkiem == null)
                                {
                                    var response = await _httpClient.PostAsJsonAsync($"https://localhost:7095/api/Voucher", voucher);
                                    if (response.IsSuccessStatusCode)
                                    {
                                        return RedirectToAction("GetAllVoucher");
                                    }
                                    return View();
                                }
                            }
                        }
                        if (voucher.SoTienCan > 0)
                        {
                            if (voucher.GiaTri <= voucher.SoTienCan)
                            {
                                if (voucher.GiaTri <= 0)
                                {
                                    ViewData["GiaTri"] = "Giá trị phải lớn hơn 0";
                                    return View();
                                }
                                if (voucher.GiaTri > 0)
                                {
                                    if (voucher.SoTienCan >= 0 && voucher.GiaTri > 0 && voucher.SoLuong > 0 && voucher.NgayKetThuc >= voucher.NgayApDung && timkiem == null)
                                    {
                                        var response = await _httpClient.PostAsJsonAsync($"https://localhost:7095/api/Voucher", voucher);
                                        if (response.IsSuccessStatusCode)
                                        {
                                            return RedirectToAction("GetAllVoucher");
                                        }
                                        return View();
                                    }
                                }
                            }
                            if (voucher.GiaTri > voucher.SoTienCan)
                            {
                                ViewData["GiaTri"] = "Giá trị phải nhỏ hơn hoặc bằng số tiền cần";
                                return View();
                            }
                        }
                    }


                }

                return View();
            }
            catch
            {
                return View();
            }
           
        }
        // update
        [HttpGet]
        public IActionResult Updates(Guid id)
        {
            try
            {
                var url = $"https://localhost:7095/api/Voucher/{id}";
                var response = _httpClient.GetAsync(url).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                var KhuyenMais = JsonConvert.DeserializeObject<VoucherView>(result);
                return View(KhuyenMais);
            }
            catch
            {
                return View();
            }
            
        }

        [HttpPost]

        public async Task<IActionResult> Updates(VoucherView voucher)
        {
            try
            {
                if (voucher.SoTienCan == 0)
                {
                    if (voucher.SoLuong > 0 && voucher.NgayKetThuc >= voucher.NgayApDung)
                    {
                        var response = await _httpClient.PutAsJsonAsync($"https://localhost:7095/api/Voucher/{voucher.Id}", voucher);
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("GetAllVoucher");
                        }
                        return View();
                    }
                    else
                    {
                        if (voucher.NgayKetThuc < voucher.NgayApDung)
                        {
                            ViewData["Ngay"] = "Ngày kết thúc phải lớn hơn ngày áp dụng";
                        }
                        if (voucher.SoLuong <= 0)
                        {
                            ViewData["SoLuong"] = "Mời bạn nhập số lượng lớn hơn 0";
                        }
                        return View();
                    }
                }
                if (voucher.SoTienCan > 0)
                {
                    if (voucher.SoTienCan >= voucher.GiaTri)
                    {
                        if (voucher.SoLuong > 0 && voucher.NgayKetThuc >= voucher.NgayApDung)
                        {
                            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7095/api/Voucher/{voucher.Id}", voucher);
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("GetAllVoucher");
                            }
                            return View();
                        }
                    }
                    if (voucher.SoTienCan < voucher.GiaTri)
                    {
                        ViewData["SoTienCan"] = "Số tiền cần phải lớn hơn giá trị";
                        return View();
                    }
                }
                else
                {
                    
                    if (voucher.SoTienCan < 0)
                    {
                        ViewData["SoTienCan"] = "Mời bạn nhập số tiền cần không âm";
                    }
                    return View();
                }
                return View();
            }
            catch
            {
                return View();
            }
            
            
        }
        // delete
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    string apiURL = $"https://localhost:7095/api/Voucher/{id}";

        //    var response = await _httpClient.DeleteAsync(apiURL);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("GetAllVoucher");
        //    }
        //    return View();
        //}
        
        public async Task<IActionResult> SuDung(Guid id)
        {
            try
            {
                var timkiem = dBContext.Vouchers.FirstOrDefault(x => x.ID == id);
                if (timkiem != null)
                {
                    timkiem.TrangThai = 1;
                    dBContext.Vouchers.Update(timkiem);
                    dBContext.SaveChanges();
                    return RedirectToAction("GetAllVoucher");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
           
        }
        public async Task<IActionResult> KoSuDung(Guid id)
        {
            try
            {
                var timkiem = dBContext.Vouchers.FirstOrDefault(x => x.ID == id);
                if (timkiem != null)
                {
                    timkiem.TrangThai = 0;
                    dBContext.Vouchers.Update(timkiem);
                    dBContext.SaveChanges();
                    return RedirectToAction("GetAllVoucher");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
            
        }
        public async Task<IActionResult> GetAllVoucherUser(Guid idkh, int productPage = 1)
        {

            try
            {
                var session = HttpContext.Session.GetString("LoginInfor");
                if (session != null)
                {
                    LoginViewModel loginViewModel = JsonConvert.DeserializeObject<LoginViewModel>(session);
                    idkh = loginViewModel.Id;
                }
            
                string apiURL = $"https://localhost:7095/api/Voucher/GetAllVoucherKH?idkh={idkh}";
                var response = await _httpClient.GetAsync(apiURL);

                if (!response.IsSuccessStatusCode)
                {
                    // Xử lý trường hợp API trả về lỗi
                    return StatusCode((int)response.StatusCode, "API error: " + response.ReasonPhrase);
                }

                var apiData = await response.Content.ReadAsStringAsync();
                var vouchers = JsonConvert.DeserializeObject<List<VoucherKH>>(apiData);

                // Lọc danh sách các voucher để chỉ lấy các voucher của khách hàng cụ thể
                var customerVouchers = vouchers.Where(x => x.IDKhachHang == idkh).ToList();

                // Gọi API để lấy thông tin chi tiết của các voucher
                string apiURL1 = $"https://localhost:7095/api/Voucher";
                var response1 = await _httpClient.GetAsync(apiURL1);

                if (!response1.IsSuccessStatusCode)
                {
                    // Xử lý trường hợp API trả về lỗi
                    return StatusCode((int)response1.StatusCode, "API error: " + response1.ReasonPhrase);
                }

                var apiData1 = await response1.Content.ReadAsStringAsync();
                var allVouchers = JsonConvert.DeserializeObject<List<VoucherView>>(apiData1);

                // Lấy thông tin chi tiết của các voucher của khách hàng từ danh sách tất cả các voucher
                var customerVouchersDetails = allVouchers
                    .Where(x => customerVouchers.Any(cv => cv.IDVoucher == x.Id))
                    .ToList();

                return View(new PhanTrangVouchers
                {
                    listvouchers = customerVouchersDetails
                        .Skip((productPage - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = productPage,
                        TotalItems = customerVouchersDetails.Count()
                    }
                });
            }
            catch (Exception ex)
            {
                // Xử lý trường hợp có lỗi xảy ra trong quá trình gọi API
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        public async Task<IActionResult> AddVoucherUser(Guid idkh, int productPage = 1)
        {
            return View();
        }

    }
}
