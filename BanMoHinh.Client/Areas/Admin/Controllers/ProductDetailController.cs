using BanMoHinh.Client.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace BanMoHinh.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductDetailController : Controller
    {
        private HttpClient _httpClient;
        Uri Url = new Uri("https://localhost:7007/api/productDetail");
        private IproductDetailApiClient _apiClient;
        public ProductDetailController(HttpClient httpClient, IproductDetailApiClient iproductDetail)
        {
            _httpClient = httpClient;
            _apiClient = iproductDetail;
        }

        public async Task<IActionResult> Show()
        {
            var response = await _httpClient.GetAsync(Url + "/get-all-productdetail");
            // Lấy dữ liệu Json trả về từ Api được call dạng string
            string apiData = await response.Content.ReadAsStringAsync();
            // Lấy kqua trả về từ API
            // Đọc từ string Json vừa thu được sang List<T>
            var colors = JsonConvert.DeserializeObject<List<ProductDetailVM>>(apiData);
            return View(colors);

        }
        public async Task<IActionResult> Create(Guid productId, Guid sizeId, Guid colorId)
        {
            var productprops = _apiClient.GetListProduct();
            ViewBag.ProductProp = productprops.Result.Select(x => new SelectListItem()
            {
                Text = x.ProductName,
                Value = x.Id.ToString(),
                Selected = productId.ToString() == x.Id.ToString()
            });
            var sizes = _apiClient.GetListSize();
            ViewBag.Size = sizes.Result.Select(x => new SelectListItem()
            {
                Text = x.SizeName,
                Value = x.Id.ToString(),
                Selected = sizeId.ToString() == x.Id.ToString()
            });
            var colors = _apiClient.GetListColor();
            ViewBag.Color = colors.Result.Select(x => new SelectListItem()
            {
                Text = x.ColorName,
                Value = x.ColorId.ToString(),
                Selected = colorId.ToString() == x.ColorId.ToString()
            });
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductDetailVM product, Guid productId, Guid sizeId, Guid colorId, string edit)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var productprops = _apiClient.GetListProduct();
            ViewBag.ProductProp = productprops.Result.Select(x => new SelectListItem()
            {
                Text = x.ProductName,
                Value = x.Id.ToString(),
                Selected = productId.ToString() == x.Id.ToString()
            });
            var sizes = _apiClient.GetListSize();
            ViewBag.Size = sizes.Result.Select(x => new SelectListItem()
            {
                Text = x.SizeName,
                Value = x.Id.ToString(),
                Selected = sizeId.ToString() == x.Id.ToString()
            });
            var colors = _apiClient.GetListColor();
            ViewBag.Color = colors.Result.Select(x => new SelectListItem()
            {
                Text = x.ColorName,
                Value = x.ColorId.ToString(),
                Selected = colorId.ToString() == x.ColorId.ToString()
            });
            var result = await _apiClient.CreateProduct(product, productId, sizeId, colorId, edit);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Show");
            }

            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var response = await _httpClient.GetAsync(Url + $"/get-{id}");
            if (response.IsSuccessStatusCode)
            {
                var apiData = await response.Content.ReadAsStringAsync();
                var co = JsonConvert.DeserializeObject<ProductDetail>(apiData);
                return View(co);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = errorMessage;
                return View();
            }
        }

        public async Task<IActionResult> Update(Guid id, ProductDetail create)
        {
            try
            {

                var response = await _httpClient.PutAsJsonAsync(Url + $"/update-productdetail-{id}", create);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Show");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = errorMessage;
                    return View();
                }

            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            var response = await _httpClient.GetAsync(Url + $"/get-{Id}");

            if (response.IsSuccessStatusCode)
            {
                var apiData = await response.Content.ReadAsStringAsync();
                var fo = JsonConvert.DeserializeObject<ProductDetail>(apiData);
                return View(fo);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = errorMessage;
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Remove(Guid Id)
        {
            var response = await _httpClient.DeleteAsync(Url + $"/delete-productdetail-{Id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
