using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace BanMoHinh.Client.Controllers
{
    public class BillController : Controller
    {
        private readonly HttpClient _httpClient;
        public Guid _billId;
        public BillController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        private string GenerateRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            return new string(Enumerable.Repeat(chars, length)
              .Select(c => c[random.Next(c.Length)]).ToArray());
        }
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {

            var orderstatus = await _httpClient.GetFromJsonAsync<List<OrderStatus>>("https://localhost:7007/api/orderstatus/getall");
            var payment = await _httpClient.GetFromJsonAsync<List<Payment>>("https://localhost:7007/api/payment/getall");
            var orders = await _httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7007/api/order/getall");
            var voucher = await _httpClient.GetFromJsonAsync<List<Voucher>>("https://localhost:7007/api/voucher/get-vouchers");
            ViewData["voucher"] = voucher;
            ViewData["payment"] = payment;
            ViewData["orderstatus"] = orderstatus;
            var cartItems = await _httpClient.GetFromJsonAsync<List<CartItem>>("https://localhost:7007/api/cartitem/Get-All-CartItem");
            ViewData["cartitem"] = cartItems;
            var response = await _httpClient.GetAsync("https://localhost:7007/api/users/getall");
            var pro = await _httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7007/api/product/get-all-product");
            ViewData["pro"] = pro;
            var allproductDetail = await _httpClient.GetFromJsonAsync<List<ProductDetailVM>>("https://localhost:7007/api/productDetail/get-all-productdetail");
            // Assuming 'user' is a collection of items
            ViewData["productdetail"] = allproductDetail;
            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadFromJsonAsync<List<User>>();
                ViewData["user"] = users;
            }
            else
            {
                // Handle errors here
                ViewData["user"] = null;
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(string RecipientName, string RecipientPhone, string RecipientAddress, string Description, Guid VoucherId, Guid PaymentId, Guid OrderStatusId, decimal total)
        {
            var orders = await _httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7007/api/order/getall");
            var allproductDetail = await _httpClient.GetFromJsonAsync<List<ProductDetailVM>>("https://localhost:7007/api/productDetail/get-all-productdetail");
            // Assuming 'user' is a collection of items
            ViewData["productdetail"] = allproductDetail;
            var orderstatus = await _httpClient.GetFromJsonAsync<List<OrderStatus>>("https://localhost:7007/api/orderstatus/getall");
            var payment = await _httpClient.GetFromJsonAsync<List<Payment>>("https://localhost:7007/api/payment/getall");
            var voucher = await _httpClient.GetFromJsonAsync<List<Voucher>>("https://localhost:7007/api/voucher/get-vouchers");
            ViewData["voucher"] = voucher;
            ViewData["payment"] = payment;
            ViewData["orderstatus"] = orderstatus;
            var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;


            int length = 8;
            string randomString = GenerateRandomString(length);
            OrderVM order = new OrderVM();  
            order.Id = Guid.NewGuid();
            order.OrderCode = randomString;
            order.UserId =  new Guid(user);
         
            order.OrderStatusId = OrderStatusId;
            order.RecipientName = RecipientName;
            order.RecipientPhone = RecipientPhone;
            order.RecipientAddress = RecipientAddress;

            order.TotalAmout =total;
            order.VoucherValue =/*Convert.ToInt32( form["VoucherValue"]);*/ 100000;
            order.TotalAmoutAfterApplyingVoucher =Convert.ToInt32( order.TotalAmout - order.VoucherValue);
            order.ShippingFee = 30000;
            DateTime orderDate = DateTime.Now;

            order.Create_Date = orderDate;
            // Khởi tạo một ngày bất kỳ

            // Tính toán ngày mới bằng cách thêm TimeSpan vào ngày hiện tại
            DateTime newDate = orderDate.Add(TimeSpan.FromDays(10)); // Thêm 1 năm


            // Gán giá trị mới cho order.Ship_Date
            order.Ship_Date = newDate;
            // Ngày đặt hàng

            // Số ngày cần thêm
            int daysToAdd = 3; // Ví dụ: nhận hàng sau 3 ngày

            // Tính ngày nhận hàng
            DateTime receiveDate = orderDate.AddDays(daysToAdd);
            order.VoucherId = VoucherId;
            order.PaymentId = PaymentId;
            // In ngày nhận hàng
            order.Delivery_Date = receiveDate;
            order.Payment_Date = receiveDate;
            order.Description = Description;
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7007/api/order/create/", order);
            _billId = order.Id;
            if (response.IsSuccessStatusCode)
            {
                var allproduct = await _httpClient.GetFromJsonAsync<List<CartItem>>("https://localhost:7007/api/cartitem/Get-All-CartItem");
                foreach (var item in allproduct)
                {
                    OrderItemVM orderItem1 = new OrderItemVM()
                    {
                        OrderId = order.Id,
                        ProductDetailId = item.ProductDetail_ID,
                        Quantity = item.Quantity,
                        Price = item.Price
                    };



                    //Chờ đợi hoàn tất yêu cầu tạo OrderItem
                    var createBillResponse = await _httpClient.PostAsJsonAsync("https://localhost:7007/api/orderitem/create/", orderItem1);
                    if (!createBillResponse.IsSuccessStatusCode)
                    {
                        var errorMessage = await createBillResponse.Content.ReadAsStringAsync();
                    }

                    //string jsonContent = JsonConvert.SerializeObject(orderItem1);

                    // Tạo nội dung yêu cầu
                    //var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    //// Gửi yêu cầu POST
                    //var responsess = await _httpClient.PostAsync("https://localhost:7007/api/orderitem/create", content);

                    //// Kiểm tra xem yêu cầu có thành công không
                    //response.EnsureSuccessStatusCode();

                    //// Đọc nội dung phản hồi
                    //string responseBody = await responsess.Content.ReadAsStringAsync();

                    // Trả về nội dung phản hồi (tùy thuộc vào cần thiết)



                    // Chờ đợi hoàn tất yêu cầu xóa CartItem
                    var deleteProduct = await _httpClient.DeleteAsync($"https://localhost:7007/api/cartitem/Delete-CartItem?id={item.Id}&proId={item.ProductDetail_ID}&cartId={item.CartId}");

                    if (!deleteProduct.IsSuccessStatusCode)
                    {
                        var errorMessage = await deleteProduct.Content.ReadAsStringAsync();
                        // Xử lý lỗi khi xóa CartItem
                    }
                    var productToUpdate = allproductDetail.FirstOrDefault(p => p.Id == orderItem1.ProductDetailId);
                    // Chờ đợi hoàn tất yêu cầu cập nhật ProductDetail
                    productToUpdate.Quantity -= orderItem1.Quantity;
                    var updateProductResponse = await _httpClient.PutAsync($"https://localhost:7007/api/productDetail/updatequantity/{orderItem1.ProductDetailId}?quantity={productToUpdate.Quantity}", null);
                    if (!updateProductResponse.IsSuccessStatusCode)
                    {
                        var errorMessage = await updateProductResponse.Content.ReadAsStringAsync();
                        // Xử lý lỗi khi cập nhật ProductDetail
                    }
                }

            }

            return View();
        }
    }
}
