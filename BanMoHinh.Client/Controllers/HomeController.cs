using BanMoHinh.Client.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;

namespace BanMoHinh.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ProductVM>>("https://localhost:7007/api/product/get-all-productvm");
            // Sắp xếp để lấy ra 4 sản phẩm mới nhất (theo ngày tạo)
            var newestProducts = result.SelectMany(p => p.ProductDvms)
                              .OrderByDescending(pd => pd.Create_At)
                              .Take(4)
                              .ToList();

            // Làm tương tự để lấy ra 4 sản phẩm bán chạy nhất
            ViewData["NewestProducts"] = newestProducts;
            if (newestProducts != null)
            {
                ViewData["NewestProducts"] = newestProducts;
            }
            else
            {
                TempData["Message"] = "Dữ liệu không tồn tại.";
            }
            //ViewData["TopSellingProducts"] = topSellingProducts;
            return View();
        }
        public IActionResult Introduct()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}