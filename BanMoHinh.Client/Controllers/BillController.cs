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

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
            var getCart = await _httpClient.GetFromJsonAsync<Cart>($"https://localhost:7007/api/cart/get-item-Cart?userId={id}");

            var listCartDetail = await _httpClient.GetFromJsonAsync<List<ViewCartDetails>>("https://localhost:7007/api/CartDetails/Get-All");
            var listcartDetailbyIdCart = listCartDetail.Where(c => c.CartId == getCart.Id);
            decimal? tongtien = 0;
            int soluong = 0;
            foreach (var item in listcartDetailbyIdCart)
            {
                tongtien += item.PriceSale * item.Quantity;
                soluong++;
            }
            TempData["TongTien"] = tongtien.ToString();
            TempData["SoLuong"] = soluong.ToString();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Checkout1(string RecipientName, string RecipientPhone, string RecipientAddress, string Description, Guid VoucherId, Guid PaymentId, Guid OrderStatusId, decimal total)
        {
            var allproductDetail = await _httpClient.GetFromJsonAsync<List<ProductDetailVM>>("https://localhost:7007/api/productDetail/get-all-productdetail");
            // Assuming 'user' is a collection of items
            ViewData["productdetail"] = allproductDetail;
            var orderstatus = await _httpClient.GetFromJsonAsync<List<OrderStatus>>("https://localhost:7007/api/orderstatus/getall");
            var voucher = await _httpClient.GetFromJsonAsync<List<Voucher>>("https://localhost:7007/api/voucher/get-vouchers");
            ViewData["voucher"] = voucher;
            ViewData["orderstatus"] = orderstatus;
            var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;


            int length = 8;
            string randomString = GenerateRandomString(length);
            OrderVM order = new OrderVM();  
            order.Id = Guid.NewGuid();
            order.OrderCode = randomString;
            order.UserId =  new Guid(user);
            order.VoucherId = VoucherId;
            order.OrderStatusId = OrderStatusId;
            order.RecipientName = RecipientName;
            order.RecipientPhone = RecipientPhone;
            order.RecipientAddress = RecipientAddress;
            foreach (var item in voucher)
            {
                if (item.Id == VoucherId)
                {
                    order.VoucherValue = item.Value;
                }
            }
            order.TotalAmout =total;
            order.TotalAmoutAfterApplyingVoucher = order.TotalAmout - order.VoucherValue;
            //order.ShippingFee = 30000;
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
            //order.PaymentId = PaymentId;
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
        [HttpPost]
        public async Task<string> CheckOut(Order hoaDon)
        {
            try
            {
                //tạo order
                // tạo order item
                // xoá sp trong giỏ hàng
                // trừ sp trong database
                // update mã giảm giá
                // update rank kh sau khi nhận hàng
                Random random = new Random();

                // Tạo chuỗi ngẫu nhiên gồm 5 ký tự
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                var order = new OrderVM() // tạo mới đối tượng order
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    OrderStatusId = Guid.Parse("1C54C2DD-2FA5-4041-9B94-FB613BEBDFBC"),
                    PaymentType = hoaDon.PaymentType,
                    VoucherId = hoaDon.VoucherId ?? null,
                    OrderCode = "KH" + new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray()),
                    RecipientName = hoaDon.RecipientName,
                    RecipientAddress = hoaDon.RecipientAddress,
                    RecipientPhone = hoaDon.RecipientPhone,
                    TotalAmout = hoaDon.TotalAmout,
                    VoucherValue = hoaDon.VoucherValue??0,
                    TotalAmoutAfterApplyingVoucher = hoaDon.TotalAmoutAfterApplyingVoucher,
                    ShippingFee = hoaDon.ShippingFee,
                    Create_Date = DateTime.Now,
                    Description = hoaDon.Description
                };
                var response = await _httpClient.PostAsJsonAsync<OrderVM>("https://localhost:7007/api/order/create", order);
                if (response.IsSuccessStatusCode)
                {
                    // get cart
                    var MyCart = await _httpClient.GetFromJsonAsync<Cart>($"https://localhost:7007/api/cart/get-item-Cart?userId={userId}");
                    // get cartitem
                    var myCartId = MyCart.Id;

                    // tạo order item
                    //foreach (var item in collection)
                    //{
                    //    var orderItem = new OrderItemVM()
                    //    {
                    //        Id = Guid.NewGuid(),
                    //        OrderId = order.Id,

                    //    }
                    //}
                   
                }

               //List<Order> lstChiTietHoaDon = new List<Order>();
               // string temp = TempData.Peek("ListBienThe") as string;
               // string trangThai = TempData.Peek("TrangThai") as string;
               // foreach (var item in JsonConvert.DeserializeObject<List<GioHangRequest>>(temp))
               // {
               //     ChiTietHoaDonViewModel chiTietHoaDon = new ChiTietHoaDonViewModel();
               //     chiTietHoaDon.IDChiTietSanPham = item.IDChiTietSanPham;
               //     chiTietHoaDon.SoLuong = item.SoLuong;
               //     chiTietHoaDon.DonGia = item.DonGia.Value;
               //     lstChiTietHoaDon.Add(chiTietHoaDon);
               // }
               // hoaDon.ChiTietHoaDons = lstChiTietHoaDon;
               // hoaDon.TrangThai = Convert.ToBoolean(trangThai);
               // var session = HttpContext.Session.GetString("LoginInfor");
               // if (session != null)
               // {
               //     hoaDon.IDNguoiDung = JsonConvert.DeserializeObject<LoginViewModel>(session).Id;
               // }
               // TempData.Remove("TongTien");
               // TempData.Remove("Quantity");
               // if (hoaDon.PhuongThucThanhToan == "COD")
               // {
               //     HttpResponseMessage response = _httpClient.PostAsJsonAsync("HoaDon", hoaDon).Result;
               //     if (response.IsSuccessStatusCode)
               //     {
               //         TempData["CheckOutSuccess"] = response.Content.ReadAsStringAsync().Result;
               //         if (!hoaDon.TrangThai) Response.Cookies.Delete("Cart");
               //         // lam them
               //         TempData["SoLuong"] = "0";
               //         // lam end
               //         return "https://localhost:5001/Home/CheckOutSuccess";
               //     }
               //     else return "";
               // }
               // else if (hoaDon.PhuongThucThanhToan == "VNPay")
               // {
               //     TempData["HoaDon"] = JsonConvert.SerializeObject(hoaDon);
               //     string vnp_Returnurl = "https://localhost:5001/Home/PaymentCallBack"; //URL nhan ket qua tra ve 
               //     string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; //URL thanh toan cua VNPAY 
               //     string vnp_TmnCode = "P4VW9FD1"; //Ma định danh merchant kết nối (Terminal Id)
               //     string vnp_HashSecret = "OPHRXNCKQAUVHIJNWXXTMPPYBVPAXUTF"; //Secret Key
               //     string ipAddr = HttpContext.Connection.RemoteIpAddress?.ToString();
               //     //Get payment input
               //     OrderInfo order = new OrderInfo();
               //     order.OrderId = DateTime.Now.Ticks; // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
               //     order.Amount = hoaDon.TongTien;
               //     order.Status = "0"; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending" khởi tạo giao dịch chưa có IPN
               //     order.CreatedDate = DateTime.Now;
               //     //Save order to db

               //     //Build URL for VNPAY
               //     VnPayLibrary vnpay = new VnPayLibrary();

               //     vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
               //     vnpay.AddRequestData("vnp_Command", "pay");
               //     vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
               //     vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString());
               //     vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
               //     vnpay.AddRequestData("vnp_CurrCode", "VND");
               //     vnpay.AddRequestData("vnp_IpAddr", ipAddr);
               //     vnpay.AddRequestData("vnp_Locale", "vn");
               //     vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OrderId);
               //     vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

               //     vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
               //     vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

               //     //Add Params of 2.1.0 Version
               //     //Billing

               //     string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
               //     //log.InfoFormat("VNPAY URL: {0}", paymentUrl);

               //     return paymentUrl;
               // }
               // else
               return "";
            }
            catch
            {
                return "https://localhost:5001/Home/CheckOutSuccess";
            }
        }
        //[HttpGet]
        //public IActionResult PaymentCallBack()
        //{
        //    try
        //    {
        //        if (Request.Query.Count > 0)
        //        {
        //            string vnp_HashSecret = "OPHRXNCKQAUVHIJNWXXTMPPYBVPAXUTF"; //Chuoi bi mat
        //            var vnpayData = Request.Query;
        //            VnPayLibrary vnpay = new VnPayLibrary();

        //            foreach (var s in vnpayData)
        //            {
        //                //get all querystring data
        //                if (!string.IsNullOrEmpty(s.Key) && s.Key.StartsWith("vnp_"))
        //                {
        //                    vnpay.AddResponseData(s.Key, vnpayData[s.Key]);
        //                }
        //            }
        //            //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
        //            //vnp_TransactionNo: Ma GD tai he thong VNPAY
        //            //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
        //            //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

        //            long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
        //            long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
        //            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
        //            string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
        //            String vnp_SecureHash = Request.Query["vnp_SecureHash"];
        //            String TerminalID = Request.Query["vnp_TmnCode"];
        //            long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
        //            String bankCode = Request.Query["vnp_BankCode"];

        //            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
        //            if (checkSignature)
        //            {
        //                if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
        //                {
        //                    //Thanh toan thanh cong
        //                    TempData.Remove("TongTien");
        //                    TempData.Remove("Quantity");
        //                    var hoaDon = JsonConvert.DeserializeObject<HoaDonViewModel>(TempData["HoaDon"].ToString());
        //                    hoaDon.NgayThanhToan = DateTime.Now;
        //                    HttpResponseMessage response = _httpClient.PostAsJsonAsync("HoaDon", hoaDon).Result;
        //                    if (response.IsSuccessStatusCode)
        //                    {
        //                        TempData["CheckOutSuccess"] = response.Content.ReadAsStringAsync().Result;
        //                        if (!hoaDon.TrangThai) Response.Cookies.Delete("Cart");
        //                        // lam them
        //                        TempData["SoLuong"] = "0";
        //                        // lam end
        //                        return RedirectToAction("CheckOutSuccess");
        //                    }
        //                    else return BadRequest();
        //                }
        //                else
        //                {
        //                    //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
        //                    return BadRequest();
        //                }
        //            }
        //            else
        //            {
        //                return BadRequest();
        //            }
        //        }
        //        else return BadRequest();
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}


        [HttpGet]
        public async Task<JsonResult> UseVoucher(string ma, int tongTien)
        {
            try
            {
                // check nằm trong danh sách áp dụng
                // check ngày bắt đầu or kết thúc
                // check giá trị bill tối thiểu

                var GetVoucher = await _httpClient.GetFromJsonAsync<Voucher>($"https://localhost:7007/api/voucher/getbycode/{ma}");
                if (GetVoucher != null)// check voucher tồn tại
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    var id = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    if (id != null)
                    {
                        var getSoHuu = await _httpClient.GetFromJsonAsync<UserVoucher>($"https://localhost:7007/api/UserVoucher/getsohuu/{GetVoucher.Id}/{id}");
                        if (getSoHuu != null)
                        {
                            if (getSoHuu.Status==true)
                            {
                                if (GetVoucher.Start_Date <= DateTime.Now )
                                {
                                    if ( GetVoucher.End_Date > DateTime.Now)
                                    {
                                            if (GetVoucher.Quantity > 0)
                                        {
                                            if (GetVoucher.Minimum_order_value < tongTien)
                                            {
                                                return Json(new { HinhThuc = GetVoucher.Discount_Type, GiaTri = GetVoucher.Value,VoucherId = GetVoucher.Id, TrangThai = true });
                                            }
                                            return Json(new { Loi = "Voucher chưa đạt đủ điều kiện: Tổng tiền sản phẩm lớn hơn " + GetVoucher.Minimum_order_value?.ToString("n0") + " VNĐ", TrangThai = false });
                                        }
                                        return Json(new { Loi = "Voucher đã sử dụng hết", TrangThai = false });
                                        }
                                    return Json(new { Loi = "Mã voucher hết hạn", TrangThai = false });
                                }
                                return Json(new { Loi = "Mã voucher chưa bắt đầu", TrangThai = false });
                             }
                            return Json(new { Loi = "Voucher đã được sử dụng", TrangThai = false });
                        }
                        return Json(new { Loi = "Bạn không nằm trong danh sách áp dụng voucher", TrangThai = false });
                    }
                }
                return Json(new { Loi = "Voucher không hợp lệ", TrangThai = false });
            }
            catch
            {
                return Json(new { HinhThuc = false, GiaTri = 0, Loi = "Voucher không hợp lệ catch" });
            }
        }
    }
}
