using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace BanMoHinh.Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
            var allproduct = await _httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7007/api/product/get-all-product");
            return View(allproduct);
        }
        public async Task<IActionResult> ProductDetailAsync(Guid id)
        {
            var allproduct = await _httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7007/api/product/get-all-product");
            var allproductDetail = await _httpClient.GetFromJsonAsync<List<ProductDetailVM>>("https://localhost:7007/api/productDetail/get-all-productdetail");
            var allProductImage = await _httpClient.GetFromJsonAsync<List<ProductImage>>("https://localhost:7007/api/productimage/get-all-productimage");
            var Product = allproduct.FirstOrDefault(x => x.Id == id);
            var productdetail = allproductDetail.FirstOrDefault(c => c.ProductId == Product.Id);
            var lstProductImage = allProductImage.Where(c => c.ProductDetailId == productdetail.Id);
            ViewData["productDetail"] = productdetail;
            ViewData["lstProductImage"] = lstProductImage;
            return View(Product);
        }
    }
}
