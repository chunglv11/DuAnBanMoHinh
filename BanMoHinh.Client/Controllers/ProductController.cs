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
        // Filters
        public async Task<List<ProductVM>> Filter(string sortOrder, List<ProductVM> lstProductVm)
        {
            switch (sortOrder)
            {
                case "best-selling":
                    lstProductVm = lstProductVm.OrderBy(p => p.ProductDvms?.Sum(d => d.Quantity)).ToList();
                    break;
                case "a":
                    lstProductVm = lstProductVm.OrderBy(p => p.ProductName).ToList();
                    break;
                case "high-price":
                    lstProductVm = lstProductVm.OrderByDescending(p => p.MaxPrice).ToList();
                    break;
                case "low-price":
                    lstProductVm = lstProductVm.OrderBy(p => p.MinPrice).ToList();
                    break;
                case "z":
                    lstProductVm = lstProductVm.OrderByDescending(p => p.ProductName).ToList();
                    break;
                default:
                    lstProductVm = lstProductVm.OrderBy(p => p.ProductName).ToList();
                    break;
            }
            return lstProductVm;
        }
        // Search
        public async Task<List<ProductVM>> Search(string name, List<ProductVM> lstProductVm)
        {
                lstProductVm = lstProductVm.Where(p => p.ProductName.ToLower().Contains(name.ToLower())).ToList();
            return lstProductVm;
        }
        // filter by form
        public async Task<List<ProductVM>> Filter(Guid?[] SelectedCategory, Guid?[] SelectedBrand, Guid?[] SelectedMaterial, int? minPrice, int? maxPrice , string? sortOrder, List<ProductVM> lstProductVm)
        {
            if (SelectedCategory.Length>0)
            {
                lstProductVm = lstProductVm.FindAll(c => SelectedCategory.Contains(c.CategoryId)).ToList();
            }
            if (SelectedBrand.Length>0)
            {
                lstProductVm = lstProductVm.FindAll(c => SelectedBrand.Contains(c.BrandId)).ToList();
            }
            if (SelectedMaterial.Length>0)
            {
                lstProductVm = lstProductVm.FindAll(c => SelectedMaterial.Contains(c.MaterialId)).ToList();
            }
            if (minPrice != null && minPrice != 0 && maxPrice != null && maxPrice != 0)
            {
                if (minPrice > maxPrice)
                {
                    int? temp = minPrice;
                    minPrice = maxPrice;
                    maxPrice = temp;
                }
                lstProductVm = lstProductVm.Where(p =>
            (minPrice >= p.MinPrice && minPrice <= p.MaxPrice) ||
            (maxPrice >= p.MinPrice && maxPrice <= p.MaxPrice))
        .ToList();
            }
            if (sortOrder!=null)
            {
                lstProductVm = await Filter(sortOrder, lstProductVm);
            }
            return lstProductVm;
        }


        public async Task<IActionResult> ListProductAsync(string? name, string? sortOrder, Guid?[] SelectedCategory, Guid?[] SelectedBrand, Guid?[] SelectedMaterial, int? minPrice, int? maxPrice)
        {

            var productCategory = await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7007/api/Category/get-all-Category");
            var productBrand = await _httpClient.GetFromJsonAsync<List<Brand>>("https://localhost:7007/api/brand/getall");
            var productMaterial = await _httpClient.GetFromJsonAsync<List<Material>>("https://localhost:7007/api/Material/getall");
            var productDetail = await _httpClient.GetFromJsonAsync<List<ProductDetailVM>>("https://localhost:7007/api/productDetail/get-all-productdetail");
            var ProductImage = await _httpClient.GetFromJsonAsync<List<ProductImage>>("https://localhost:7007/api/productimage/get-all-productimage");

            List<SelectListItem> selectListItemsProductCategory = productCategory.Select(category => new SelectListItem
            {
                Value = category.Id.ToString(),
                Text = category.CategoryName
            }).ToList();
            List<SelectListItem> selectListItemsProductMaterial = productMaterial.Select(material => new SelectListItem
            {
                Value = material.Id.ToString(),
                Text = material.MaterialName
            }).ToList();
            List<SelectListItem> selectListItemsProductBrand = productBrand.Select(brand => new SelectListItem
            {
                Value = brand.Id.ToString(),
                Text = brand.BrandName
            }).ToList();

            ViewData["productCategory"] = selectListItemsProductCategory;
            ViewData["productBrand"] = selectListItemsProductBrand;
            ViewData["productMaterial"] = selectListItemsProductMaterial;
            ViewData["productDetail"] = productDetail;
            ViewData["ProductImage"] = ProductImage;

            var allproduct = await _httpClient.GetFromJsonAsync<List<ProductVM>>("https://localhost:7007/api/product/get-all-productvm");
            allproduct = allproduct.GroupBy(p => new { p.ProductName }).Select(g => g.First()).Where(c => productDetail.Any(b => b.ProductId == c.Id)).ToList();
            if (!string.IsNullOrWhiteSpace(name))
            {
                allproduct = await Search(name, allproduct);
            }
            allproduct = await Filter(SelectedCategory, SelectedBrand, SelectedMaterial, minPrice, maxPrice, sortOrder, allproduct);
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
