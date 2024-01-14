using BanMoHinh.Share.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Web;

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
        //loại bỏ thẻ html
        //private string RemoveHtmlTags(string html)
        //{
        //    var doc = new HtmlDocument();
        //    doc.LoadHtml(html);

        //    return doc.DocumentNode.InnerText;
        //}
        //public string ReplaceUnicodeCharacters(string input)
        //{
        //    if (string.IsNullOrEmpty(input))
        //    {
        //        return input;
        //    }

        //    string normalized = input.Normalize(NormalizationForm.FormKD);
        //    Encoding removal = Encoding.GetEncoding(Encoding.UTF8.CodePage,
        //                                            new EncoderReplacementFallback(""),
        //                                            new DecoderReplacementFallback(""));
        //    byte[] bytes = removal.GetBytes(normalized);
        //    string asciiString = Encoding.UTF8.GetString(bytes);

        //    // Giải mã ký tự HTML
        //    return HttpUtility.HtmlDecode(asciiString);
        //}
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
        public async Task<IActionResult> CreateProduct(Product pro, string edit)
        {
            try
            {
                if (pro.CategoryId == null || pro.BrandId == null || pro.CategoryId == null || pro.ProductName == null )
                {
                    ViewData["Null"] = "Không được để trống";
                }
                var allproduct = await _httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7007/api/product/get-all-product");
                if (pro.CategoryId != null || pro.BrandId != null || pro.CategoryId != null || pro.ProductName != null)
                {
                    if (pro.ProductName.ToLower() == allproduct.FirstOrDefault().ProductName.ToLower())
                    {
                        ViewData["Nam"] = "Đã có sản phẩm này";
                    }
                    pro.Status = true;
                    pro.Long_Description = edit;
                    pro.Create_At = DateTime.Now;
                    pro.AvailableQuantity = 0;
                    pro.Update_At = null;
                    var createpro = await _httpClient.PostAsJsonAsync("https://localhost:7007/api/product/create-product", pro);
                    if (createpro.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAllProduct");
                    }
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
        public async Task<IActionResult> EditProduct(Product pro, string edit)
        {
            try
            {
                pro.Long_Description = edit;
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
