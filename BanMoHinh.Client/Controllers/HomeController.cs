using BanMoHinh.Client.Models;
using BanMoHinh.Share.Models;
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
            var productDetail = await _httpClient.GetFromJsonAsync<List<ProductDetailVM>>("https://localhost:7007/api/productDetail/get-all-productdetail");
            var ProductImage = await _httpClient.GetFromJsonAsync<List<ProductImage>>("https://localhost:7007/api/productimage/get-all-productimage");
            // Sắp xếp để lấy ra 4 sản phẩm mới nhất (theo ngày tạo)
            var newestProducts = result
            .OrderByDescending(pd => pd.Create_At)
            .GroupBy(pd => new { pd.BrandId, pd.MaterialId, pd.CategoryId }) // Nhóm theo các thuộc tính 
            .Select(group => group.First()) // Lấy phần tử đầu tiên từ mỗi nhóm
            .Take(4)
            .ToList();

            ViewData["NewestProducts"] = newestProducts;
            ViewData["productDetail"] = productDetail;
            ViewData["ProductImage"] = ProductImage;
            if (newestProducts != null)
            {
                ViewData["NewestProducts"] = newestProducts;
            }
            else
            {
                TempData["Message"] = "Dữ liệu không tồn tại.";
            }
            // sản phẩm bán chạy
            var HDCT = await _httpClient.GetFromJsonAsync<List<OrderItem>>("https://localhost:7007/api/orderitem/getall");
            var query =
            (from item in HDCT
             group item.Quantity by item.productDetail into g
             orderby g.Sum() descending
             select new { ProductDetails = g.Key }).Take(4).ToList();
            //var bestSell = from a in query
            //               join b in result on a.ProductDetails.ProductId equals b.Id into temp
            //               from b in temp.DefaultIfEmpty()
            //               join c in ProductImage on a.ProductDetails.Id equals c.ProductDetailId into tempImage
            //               from c in tempImage.DefaultIfEmpty()
            //               select new ProductDetailVM()
            //               {
            //                   Id = a.ProductDetails.Id,
            //                   ProductName = b != null ? b.ProductName : "Unknown", // Xử lý trường hợp b là null
            //                   PriceSale = a.ProductDetails.PriceSale
            //               };

            ViewData["TopSellingProducts"] = query;
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