using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                    var userName = userIdClaim.Value;
                    var getUserbyName = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7007/api/users/get/{userName}");
                    var getCart = await _httpClient.GetFromJsonAsync<Cart>($"https://localhost:7007/api/cart/get-item-Cart?userId={getUserbyName.Id}");
                    var listCartDetail = await _httpClient.GetFromJsonAsync<List<CartItem>>("https://localhost:7007/api/cartitem/Get-All-CartItem");
                    var listcartDetailbyIdCart = listCartDetail.Where(c => c.CartId == getCart.Id);
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
        [HttpPost]
        public async Task<IActionResult> AddtoCart(Guid prDetailId, int quantity, int Price)
        {
            //prDetailId = Guid.Parse("51d38d0f-686e-49bc-9005-7779a7c47200");
            //quantity = 1;
            //Price = 123;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.Name);
                if (userIdClaim != null)
                {
                    var userName = userIdClaim.Value;
                    var getUserbyName = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7007/api/users/get/{userName}");
                    var getProductdetailbyID = await _httpClient.GetFromJsonAsync<ProductDetailVM>($"https://localhost:7007/api/productDetail/get/{prDetailId}");

                    var getCart = await _httpClient.GetFromJsonAsync<Cart>($"https://localhost:7007/api/cart/get-item-Cart?userId={getUserbyName.Id}");
                    List<CartItem> getAllCartItem = await _httpClient.GetFromJsonAsync<List<CartItem>>("https://localhost:7007/api/cartitem/Get-All-CartItem");
                    IEnumerable<CartItem> CartItembyidCart = getAllCartItem.Where(c => c.ProductDetail_ID == prDetailId);

                    var ProductInCart = CartItembyidCart.FirstOrDefault(c => c.ProductDetail_ID == prDetailId);
                    if (ProductInCart != null)
                    {
                        // cong them so luong san pham 
                        ProductInCart.Quantity += quantity;
                        var updateResponse = await _httpClient.PutAsJsonAsync($"https://localhost:7007/api/cartitem/Update-CartItem?id={ProductInCart.Id}", ProductInCart);
                        return RedirectToAction("ShowCart", "Cart");
                    }
                    else
                    {
                        if (getUserbyName != null)
                        {
                            // creat Cartdetail
                            CartItem cartItem = new CartItem();
                            cartItem.ProductDetail_ID = prDetailId;
                            cartItem.Id = Guid.NewGuid();
                            cartItem.CartId = getCart.Id;

                            if (getProductdetailbyID.Quantity >= quantity)
                            {
                                cartItem.Quantity = quantity;

                            }
                            else
                            {// load lai trang
                                Console.WriteLine("so luong san pham het r ban oi");
                                return RedirectToAction("ProductDetail", "Product", new { id = prDetailId });
                            }
                            cartItem.Price = Price;
                            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7007/api/cartitem/Insert-Cart-Item", cartItem);
                            if (response.IsSuccessStatusCode)
                            {
                                Console.WriteLine("Đã tạo CartItem thành công.");
                                // return ve gio hang 
                                return RedirectToAction("ShowCart", "Cart");
                            }
                            else
                            {
                                Console.WriteLine("Lỗi trong việc tạo CartItem. Mã trạng thái: " + response.StatusCode);
                                return RedirectToAction("Error");
                            }
                        }
                        else
                        {
                            // ban chua dang nhap cho em no ra cho dang nhao anh oi
                            return RedirectToAction("Login", "Authentication");
                        }
                    }
                }
                else
                { // chua biet
                    return RedirectToAction("Login", "Authentication");
                }
            }
            else
            {
                // ban chua dang nhap cho em no ra cho dang nhao anh oi
                return RedirectToAction("Login", "Authentication");
            }
        }
    }
}
