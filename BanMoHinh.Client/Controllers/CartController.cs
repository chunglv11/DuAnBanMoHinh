using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Policy;
using static System.Net.WebRequestMethods;

namespace BanMoHinh.Client.Controllers
{
    public class CartController : Controller
    {
        private readonly HttpClient _httpClient;
        public CartController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> ShowCart()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.Name);
                if (userIdClaim != null)
                {
                    var getColor = await _httpClient.GetFromJsonAsync<List<Colors>>("https://localhost:7007/api/color/get-all-Color");
                    var getCate = await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7007/api/Category/get-all-Category");
                    var getSize = await _httpClient.GetFromJsonAsync<List<Size>>("https://localhost:7007/api/size/get-all-size");
                    var id = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var getCart = await _httpClient.GetFromJsonAsync<Cart>($"https://localhost:7007/api/cart/get-item-Cart?userId={id}");

                    var listCartDetail = await _httpClient.GetFromJsonAsync<List<ViewCartDetails>>("https://localhost:7007/api/CartDetails/Get-All");
                    List<ViewCartDetails>? listcartDetailbyIdCart = listCartDetail.Where(c => c.CartId == getCart.Id).ToList();
                    var getAllProductDetail = await _httpClient.GetFromJsonAsync<List<ProductDetail>>("https://localhost:7007/api/productDetail/get-all-productdetail"); // lấy hết spct
                    var productDetailCheck = getAllProductDetail.Where(productDetail =>listcartDetailbyIdCart.Any(cartDetail =>cartDetail.ProductDetail_Id == productDetail.Id && cartDetail.Quantity > productDetail.Quantity)).ToList();
                    // lấy sp sao cho sl trong cart> sl kho
                    var listcartDetailbyIdCartJson = JsonConvert.SerializeObject(listcartDetailbyIdCart);
                    // Lưu chuỗi JSON vào TempData
                    TempData["Cart"] = listcartDetailbyIdCartJson;
                    ViewData["productDetailCheck"] = productDetailCheck;
                    ViewData["color"] = getColor;
                    ViewData["size"] = getSize;
                    return View(listcartDetailbyIdCart);
                }
                else
                {
                    return RedirectToAction("Login", "Authentication");
                }
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }

        }
		[HttpPost]
		public async Task<JsonResult> AddtoCart(string ProductName, Guid colorId, Guid sizeId, int quantity)
        {
			//

			if (colorId == Guid.Parse("00000000-0000-0000-0000-000000000000") || sizeId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
				return Json(new { message = "Vui lòng chọn biến thể", status = false });
			}
            else
            {
				var identity = HttpContext.User.Identity as ClaimsIdentity;
				if (identity != null)
				{
					var userID = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value); // userId
					var MyCart = await _httpClient.GetFromJsonAsync<Cart>($"https://localhost:7007/api/cart/get-item-Cart?userId={userID}");// get my cart                 
					var ItemInMyCart = await _httpClient.GetFromJsonAsync<List<CartItem>>($"https://localhost:7007/api/cartitem/getcartitembycartid?cartid={MyCart.Id}"); // lấy item in cart
					var prodctDetailViewModel = await _httpClient.GetFromJsonAsync<List<ProductDetailVM>>("https://localhost:7007/api/productDetail/get-all-productdetail"); // get productdetail model
					var ProductDetailToAddCart = prodctDetailViewModel.FirstOrDefault(c => c.ProductName == ProductName && c.ColorId == colorId && c.SizeId == sizeId);
					// lấy được product detail để add cart, lấy được cart item
					// check exist trong cart item
					var checkExistInCartItem = ItemInMyCart.Where(c => c.ProductDetail_ID == ProductDetailToAddCart.Id);
					if (checkExistInCartItem.Count() != 1) // nếu sp không tồn tại trong cart item
					{
						var cartItem = new CartItem()
						{
							ProductDetail_ID = ProductDetailToAddCart.Id,
							CartId = MyCart.Id,
							Price = (int)(ProductDetailToAddCart.PriceSale),
						};
						if (ProductDetailToAddCart.Quantity < quantity)
						{
							return Json(new { message = "Vui lòng chọn lại số lượng nhỏ hơn số lượng sản phẩm tồn!!", status = false });
						}
                        if (ProductDetailToAddCart.Quantity <= 0)
						{
							return Json(new { message = "Sản phẩm đã hết hàng!!", status = false });
						}

						else
						{
							if (ProductDetailToAddCart.Quantity < quantity)
							{
								return Json(new { message = "Vui lòng chọn lại số lượng nhỏ hơn số lượng sản phẩm tồn!!", status = false });
							}
							if (ProductDetailToAddCart.Quantity <= 0)
							{
								return Json(new { message = "Sản phẩm đã hết hàng!!", status = false });
							}

							cartItem.Quantity = quantity;
							var response = await _httpClient.PostAsJsonAsync("https://localhost:7007/api/cartitem/Insert-Cart-Item", cartItem);
							if (!response.IsSuccessStatusCode)
							{
								return Json(new { message = "Thêm sản phẩm thất bại!!", status = false });
							}
							return Json(new { message = "Thêm thành công vào giỏ hàng", status = true });
						}

					}
					else
					{
						if (ProductDetailToAddCart.Quantity < quantity)
						{
							return Json(new { message = "Vui lòng chọn lại số lượng nhỏ hơn số lượng sản phẩm tồn!!", status = false });
						}
						if (ProductDetailToAddCart.Quantity <= 0)
						{
							return Json(new { message = "Sản phẩm đã hết hàng!!", status = false });
						}

						var productDetailInCart = checkExistInCartItem.FirstOrDefault();
						productDetailInCart.Quantity += quantity;
						var updateResponse = await _httpClient.PutAsJsonAsync($"https://localhost:7007/api/cartitem/Update-CartItem?id={productDetailInCart.Id}", productDetailInCart);
						if (!updateResponse.IsSuccessStatusCode)
						{
							return Json(new { message = "Vui lòng chọn biến thể", status = false });
						}
						return Json(new { message = "Thêm thành công", status = true });
					}

				}
				else
				{
					// ban chua dang nhap cho em no ra cho dang nhao anh oi
					return Json(new { message = "Vui lòng đăng nhập để mua hàng", status = false });
				}
			}
           
        }
        public async Task<IActionResult> TangSL(Guid id, Guid idCartItem, Guid idgh)
        {
            try
            {
                var url = $"https://localhost:7007/api/CartDetails/TangSl?id={id}&idCartItem={idCartItem}&idgh={idgh}";
                var response = await _httpClient.GetAsync(url);
                return RedirectToAction("ShowCart");
            }
            catch (Exception)
            {
                return View("Error");
            }

        }
        public async Task<IActionResult> GiamSL(Guid id, Guid idCartItem, Guid idgh)
        {
            try
            {
                var url = $"https://localhost:7007/api/CartDetails/GiamSL?id={id}&idCartItem={idCartItem}&idgh={idgh}";
                var response = await _httpClient.GetAsync(url);
                return RedirectToAction("ShowCart");
            }
            catch (Exception)
            {
                return View("Error");
            }

        }
        [HttpPost]
        public async Task<JsonResult> UpdateSLInCart(Guid idProductDetail, int newQuantity)
        {
           var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userID = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
            var MyCart = await _httpClient.GetFromJsonAsync<Cart>($"https://localhost:7007/api/cart/get-item-Cart?userId={userID}");
            var ListCartItem = await _httpClient.GetFromJsonAsync<List<CartItem>>($"https://localhost:7007/api/cartitem/getcartitembycartid?cartid={MyCart.Id}");
            var cartItem = ListCartItem.FirstOrDefault(c => c.ProductDetail_ID == idProductDetail); // cartitemid
            var ProductDetail = await _httpClient.GetFromJsonAsync<ProductDetailVM>($"https://localhost:7007/api/productDetail/get/{idProductDetail}"); // sp kho
            if (ProductDetail.Quantity< newQuantity)
            {
                return Json(new { message = "Sản phẩm vượt quá giới hạn trong kho", status = false });
            }
            if (newQuantity <=0)
            {
                return Json(new { message = "Sản phẩm tối thiểu là 1", status = false });
            }
            var Response = await _httpClient.GetAsync($"https://localhost:7007/api/cartitem/UpdateQuantityCartItem?cartItemId={cartItem.Id}&quantity={newQuantity}");
            if (Response.IsSuccessStatusCode)
            {
                return Json(new { message ="OK", status = true });
            }
            return Json(new { message = "Lỗi không xác định", status = false });
        }
        [HttpGet]
        public async Task<JsonResult> DeleteAllItemInCart()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userID = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                var MyCart = await _httpClient.GetFromJsonAsync<Cart>($"https://localhost:7007/api/cart/get-item-Cart?userId={userID}");
                var url = $"https://localhost:7007/api/cartitem/Delete-CartItem?cartId={MyCart.Id}";
                var response = await _httpClient.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { message = "Xoá giỏ hàng thành công!!!", status = true });
                }
                return Json(new { message = "Lỗi không xác định", status = false });
            }
            catch (Exception)
            {
                return Json(new { message = "Lỗi không xác định", status = false });
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteItemInCart(Guid ProductDetailId)
        {
            try
            {
			
				var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userID = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                var MyCart = await _httpClient.GetFromJsonAsync<Cart>($"https://localhost:7007/api/cart/get-item-Cart?userId={userID}");
                var lstCartItem = await _httpClient.GetFromJsonAsync<List<CartItem>>($"https://localhost:7007/api/cartitem/getcartitembycartid?cartid={MyCart.Id}");
                var cartItem = lstCartItem.FirstOrDefault(c => c.ProductDetail_ID == ProductDetailId);

				var url = $"https://localhost:7007/api/CartDetails/Delete/{ProductDetailId}?idCartItem={cartItem.Id}&idgh={MyCart.Id}";
                var response = await _httpClient.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                return Json(new { message = "Xoá giỏ hàng thành công!!!", status = true });
                }
                return Json(new { message = "Lỗi không xác định", status = false });
            }
            catch (Exception)
            {
                return Json(new { message = "Lỗi không xác định", status = false });

            }

        }


    }
}
