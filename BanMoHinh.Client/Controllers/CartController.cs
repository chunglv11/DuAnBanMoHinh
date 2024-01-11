using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Security.Claims;
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
                    var listcartDetailbyIdCartJson = JsonConvert.SerializeObject(listcartDetailbyIdCart);
                    // Lưu chuỗi JSON vào TempData
                    TempData["Cart"] = listcartDetailbyIdCartJson;
                    ViewData["color"] = getColor;
                    ViewData["size"] = getSize;
                    return View(listcartDetailbyIdCart);
                }
                else
                {
                    // ban chua dang nhap cho em no ra cho dang nhao anh oi
                    return RedirectToAction("Login", "Authentication");
                }
            }
            else
            {
                // ban chua dang nhap cho em no ra cho dang nhao anh oi
                return RedirectToAction("Login", "Authentication");
            }

        }
        public async Task<IActionResult> AddtoCart(string ProductName, Guid colorId, Guid sizeId, int quantity)
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
                    if (checkExistInCartItem.Count()!=1) // nếu sp không tồn tại trong cart item
                    {
                        var cartItem = new CartItem()
                        {
                            ProductDetail_ID = ProductDetailToAddCart.Id,
                            CartId = MyCart.Id,
                            Price = (int)(ProductDetailToAddCart.PriceSale),
                        };
                        if (ProductDetailToAddCart.Quantity<quantity)
                        {
                            return BadRequest("Số lượng k đủ trong kho");
                        }
                        else
                        {
                            cartItem.Quantity = quantity;
                            var response = await _httpClient.PostAsJsonAsync("https://localhost:7007/api/cartitem/Insert-Cart-Item", cartItem);
                            if (!response.IsSuccessStatusCode)
                            {
                                return BadRequest("Lỗi Post sp ");
                            }
                            return RedirectToAction("ShowCart", "Cart");
                        }

                    }
                    else
                    {
                        var productDetailInCart =  checkExistInCartItem.FirstOrDefault();
                        productDetailInCart.Quantity += quantity;
                        var updateResponse = await _httpClient.PutAsJsonAsync($"https://localhost:7007/api/cartitem/Update-CartItem?id={productDetailInCart.Id}", productDetailInCart);
                        if (!updateResponse.IsSuccessStatusCode)
                        {
                            return BadRequest("Lỗi Post sp ");
                        }
                        return RedirectToAction("ShowCart", "Cart");
                    }

            }
            else
            {
                // ban chua dang nhap cho em no ra cho dang nhao anh oi
                return RedirectToAction("Login", "Authentication");
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
        public async Task<IActionResult> DeleteItemInCart(Guid id, Guid idCartItem, Guid idgh)
        {
            try
            {
                var url = $"https://localhost:7007/api/CartDetails/Delete/{id}?idCartItem={idCartItem}&idgh={idgh}";
                var response = await _httpClient.DeleteAsync(url);
                return RedirectToAction("ShowCart");
            }
            catch (Exception)
            {
                return View("Error");
            }

        }
    }
}
