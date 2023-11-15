using BanMoHinh.Client.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace BanMoHinh.Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        private IproductDetailApiClient _apiClient;

        public ProductController(HttpClient httpClient, IproductDetailApiClient apiClient)
        {
            _httpClient = httpClient;
            _apiClient = apiClient;
        }


        //dg lỗi
        public async Task<IActionResult> Search(string name)
        {
            var productDetail = await _httpClient.GetFromJsonAsync<List<ProductDetailVM>>("https://localhost:7007/api/productDetail/get-all-productdetail");
            var allproduct = await _httpClient.GetFromJsonAsync<List<ProductVM>>("https://localhost:7007/api/product/get-all-productvm");
            //allproduct = allproduct.GroupBy(p => new { p.ProductName }).Select(g => g.First()).Where(c => productDetail.Any(b => b.ProductId == c.Id)).ToList();
            if (string.IsNullOrEmpty(name))
            {
                // Nếu không có tên sản phẩm được cung cấp, trả về tất cả sản phẩm


                return View(allproduct);
            }
            else
            {
                // Nếu tên sản phẩm được cung cấp, thực hiện tìm kiếm
                var searchResult = allproduct.Where(p => p.ProductName.ToLower().Contains(name.ToLower())).ToList();

                return View("ListProduct", searchResult);
            }
        }

        public async Task<JsonResult> WishList(Guid ProductId)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    var userIdClaim = identity.FindFirst(ClaimTypes.Name);

                    if (userIdClaim != null)
                    {
                        var userName = userIdClaim.Value;
                        var getUserbyName = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7007/api/users/get/{userName}");

                        if (getUserbyName != null)
                        {
                            var userId = getUserbyName.Id;


                            // Tạo yêu cầu để thêm sản phẩm vào danh sách yêu thích
                            var requestData = new
                            {
                                UserId = userId,
                                ProductId = ProductId
                            };

                            // Gửi yêu cầu POST đến API
                            var response = await _httpClient.PostAsJsonAsync("https://localhost:7007/api/WishList/create-wishlist", requestData);

                            // Kiểm tra xem yêu cầu có thành công không
                            if (response.IsSuccessStatusCode)
                            {
                                return Json(new { success = true });
                            }
                            else
                            {
                                // Xử lý khi có lỗi từ API khi thêm vào danh sách yêu thích
                                return Json(new { success = false, errorMessage = "Có lỗi khi thêm vào danh sách yêu thích." });
                            }
                        }
                    }
                }

                return Json(new { success = false, errorMessage = "Không thể xác định người dùng." });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }




        public async Task<IActionResult> ShowWishList()
        {
            var wl = await _httpClient.GetFromJsonAsync<List<WishListVM>>("https://localhost:7007/api/WishList/get-all");
            ViewData["WishList"] = wl;
            return View(wl);
        }
        public async Task<IActionResult> Filter(string sortOrder)
        {
            var productCategory = await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7007/api/Category/get-all-Category");
            var productBrand = await _httpClient.GetFromJsonAsync<List<Brand>>("https://localhost:7007/api/brand/getall");
            var productMaterial = await _httpClient.GetFromJsonAsync<List<Material>>("https://localhost:7007/api/Material/getall");
            var productDetail = await _httpClient.GetFromJsonAsync<List<ProductDetailVM>>("https://localhost:7007/api/productDetail/get-all-productdetail");
            var ProductImage = await _httpClient.GetFromJsonAsync<List<ProductImage>>("https://localhost:7007/api/productimage/get-all-productimage");

            ViewData["productCategory"] = productCategory;
            ViewData["productBrand"] = productBrand;
            ViewData["productMaterial"] = productMaterial;
            ViewData["productDetail"] = productDetail;
            ViewData["ProductImage"] = ProductImage;
            var allproduct = await _httpClient.GetFromJsonAsync<List<ProductVM>>("https://localhost:7007/api/product/get-all-productvm");
            allproduct = allproduct.GroupBy(p => new { p.ProductName }).Select(g => g.First()).Where(c => productDetail.Any(b => b.ProductId == c.Id)).ToList();
            switch (sortOrder)
            {
                case "best-selling":
                    allproduct = allproduct.OrderBy(p => p.ProductDvms?.Sum(d => d.Quantity)).ToList();
                    break;
                case "a":
                    allproduct = allproduct.OrderBy(p => p.ProductName).ToList();
                    break;
                case "high-price":
                    allproduct = allproduct.OrderByDescending(p => p.MaxPrice).ToList();
                    break;
                case "low-price":
                    allproduct = allproduct.OrderBy(p => p.MinPrice).ToList();
                    break;
                case "z":
                    allproduct = allproduct.OrderByDescending(p => p.ProductName).ToList();
                    break;
                default:
                    allproduct = allproduct.OrderBy(p => p.ProductName).ToList();
                    break;
            }
            return View("ListProduct", allproduct);
        }
        public async Task<IActionResult> ListProductAsync()
        {

            var productCategory = await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7007/api/Category/get-all-Category");
            var productBrand = await _httpClient.GetFromJsonAsync<List<Brand>>("https://localhost:7007/api/brand/getall");
            var productMaterial = await _httpClient.GetFromJsonAsync<List<Material>>("https://localhost:7007/api/Material/getall");
            var productDetail = await _httpClient.GetFromJsonAsync<List<ProductDetailVM>>("https://localhost:7007/api/productDetail/get-all-productdetail");
            var ProductImage = await _httpClient.GetFromJsonAsync<List<ProductImage>>("https://localhost:7007/api/productimage/get-all-productimage");

            ViewData["productCategory"] = productCategory;
            ViewData["productBrand"] = productBrand;
            ViewData["productMaterial"] = productMaterial;
            ViewData["productDetail"] = productDetail;
            ViewData["ProductImage"] = ProductImage;
            var allproduct = await _httpClient.GetFromJsonAsync<List<ProductVM>>("https://localhost:7007/api/product/get-all-productvm");
            allproduct = allproduct.GroupBy(p => new { p.ProductName }).Select(g => g.First()).Where(c => productDetail.Any(b => b.ProductId == c.Id)).ToList();
            return View(allproduct);
        }
        public async Task<IActionResult> ProductDetailAsync(Guid id)
        {
            var allproduct = await _httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7007/api/product/get-all-product");
            var allproductDetail = await _httpClient.GetFromJsonAsync<List<ProductDetailVM>>("https://localhost:7007/api/productDetail/get-all-productdetail");
            var allProductImage = await _httpClient.GetFromJsonAsync<List<ProductImage>>("https://localhost:7007/api/productimage/get-all-productimage");
            var Product = allproduct.FirstOrDefault(x => x.Id == id);
            var productdetail = allproductDetail.FirstOrDefault(c => c.ProductId == Product.Id);
            var lstProductImage = allProductImage.Where(c => c.ProductDetailId == productdetail.Id).ToList();
            ViewData["productDetail"] = productdetail;
            ViewData["lstProductImage"] = lstProductImage;
            return View(Product);

        }

    }
}
