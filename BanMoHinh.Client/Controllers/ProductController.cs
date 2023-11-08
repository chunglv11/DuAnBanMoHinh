using BanMoHinh.Client.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using System.Net.Http;

namespace BanMoHinh.Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        private IproductDetailApiClient _apiClient;

        public ProductController(HttpClient httpClient, IproductDetailApiClient iproductDetailApiClient)
        {
            _httpClient = httpClient;
            _apiClient = iproductDetailApiClient;
        }
        //dg lỗi
        public async Task<IActionResult> Search(string name)
        {
            var productDetail = await _httpClient.GetFromJsonAsync<List<ProductDetailVM>>("https://localhost:7007/api/productDetail/get-all-productdetail");
            var allproduct = await _httpClient.GetFromJsonAsync<List<ProductVM>>("https://localhost:7007/api/product/get-all-productvm");
            allproduct = allproduct.GroupBy(p => new { p.ProductName }).Select(g => g.First()).Where(c => productDetail.Any(b => b.ProductId == c.Id)).ToList();
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
