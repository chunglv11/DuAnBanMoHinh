using BanMoHinh.Share.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // GET: ProductController
        public async Task<IActionResult> GetAllProduct()
        {
            var productCategory = await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7007/api/Category/get-all-Category");
            var productBrand = await _httpClient.GetFromJsonAsync<List<Brand>>("https://localhost:7007/api/brand/getall");
            var productMaterial = await _httpClient.GetFromJsonAsync<List<Material>>("https://localhost:7007/api/Material/getall");

            ViewData["productCategory"] = productCategory;
            ViewData["productBrand"] = productBrand;
            ViewData["productMaterial"] = productMaterial;
            var allproduct = await _httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7007/api/product/get-all-product");
            return View(allproduct);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public async Task<IActionResult> CreateProduct()
        {
            var productCategory = await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7007/api/Category/get-all-Category");
            var productBrand = await _httpClient.GetFromJsonAsync<List<Brand>>("https://localhost:7007/api/brand/getall");
            var productMaterial = await _httpClient.GetFromJsonAsync<List<Material>>("https://localhost:7007/api/Material/getall");

            ViewData["productCategory"] = productCategory;
            ViewData["productBrand"] = productBrand;
            ViewData["productMaterial"] = productMaterial;

            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product pro)
        {
            try
            {
                pro.Status = true;
                var createpro = await _httpClient.PostAsJsonAsync("https://localhost:7007/api/product/create-product", pro);
                if (createpro.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllProduct");
                }
                return RedirectToAction("CreateProduct");
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> DetailProDuct(Guid id)
        {
            var productCategory = await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7007/api/Category/get-all-Category");
            var productBrand = await _httpClient.GetFromJsonAsync<List<Brand>>("https://localhost:7007/api/brand/getall");
            var productMaterial = await _httpClient.GetFromJsonAsync<List<Material>>("https://localhost:7007/api/Material/getall");

            ViewData["productCategory"] = productCategory;
            ViewData["productBrand"] = productBrand;
            ViewData["productMaterial"] = productMaterial;

            var result = await _httpClient.GetFromJsonAsync<Product>($"https://localhost:7007/api/product/get-{id}");

            return View(result);
        }
        // GET: ProductController/Edit/5
        public async Task<IActionResult> EditProduct(Guid id)
        {
            var productCategory = await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7007/api/Category/get-all-Category");
            var productBrand = await _httpClient.GetFromJsonAsync<List<Brand>>("https://localhost:7007/api/brand/getall");
            var productMaterial = await _httpClient.GetFromJsonAsync<List<Material>>("https://localhost:7007/api/Material/getall");

            ViewData["productCategory"] = productCategory;
            ViewData["productBrand"] = productBrand;
            ViewData["productMaterial"] = productMaterial;
            var result = await _httpClient.GetFromJsonAsync<Product>($"https://localhost:7007/api/product/get-{id}");

            return View(result);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(Product pro)
        {
            try
            {
                var result = await _httpClient.PutAsJsonAsync($"https://localhost:7007/api/product/update-product-{pro.Id}", pro);
                return RedirectToAction("GetAllProduct");
            }
            catch
            {
                return View();
            }
        }

        // POST: ProductController/Delete/5
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7007/api/product/delete-product-{id}");
            return RedirectToAction("GetAllProduct");
        }
    }
}
