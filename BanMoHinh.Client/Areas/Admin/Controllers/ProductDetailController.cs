using BanMoHinh.Client.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace BanMoHinh.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductDetailController : Controller
    {
        private IproductDetailApiClient _apiClient;
        public ProductDetailController(IproductDetailApiClient iproductDetail)
        {
            _apiClient = iproductDetail;
        }

        public async Task<IActionResult> Show()
        {
            var response = await _apiClient.GetAllProductDetail();
            return View(response);

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
        public async Task<IActionResult> Update(Guid id, Guid sizeId, Guid colorId)
        {
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
            var response = await _apiClient.GetByIdProductDetail(id);
            return View(response);
        }

        public async Task<IActionResult> Update(ProductDetailVM create, Guid sizeId, Guid colorId, string edit)
        {
            try
            {
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
                var response = await _apiClient.UpdateProduct(create, sizeId, colorId, edit);
                if (response)
                {
                    return RedirectToAction("Show");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
                    return View(create);
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

            var result = await _apiClient.GetByIdProductDetail(Id);
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Remove(Guid Id)
        {
            var response = await _apiClient.DeleteProductDetail(Id);

            if (response)
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
