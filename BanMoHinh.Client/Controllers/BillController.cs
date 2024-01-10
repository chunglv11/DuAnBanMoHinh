﻿using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace BanMoHinh.Client.Controllers
{
    public class BillController : Controller
    {
        private readonly HttpClient _httpClient;
        public Guid _billId;
        public Guid _orderId ;
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
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                var order = new OrderVM(); // tạo mới đối tượng order
                order.Id = Guid.NewGuid();
                order.UserId = userId;
                order.PaymentType = hoaDon.PaymentType;
                order.VoucherId = hoaDon.VoucherId ?? null;
                order.OrderCode = "KH" + GenerateRandomString(5);
                order.RecipientName = hoaDon.RecipientName;
                order.RecipientAddress = hoaDon.RecipientAddress;
                order.RecipientPhone = hoaDon.RecipientPhone;
                order.TotalAmout = hoaDon.TotalAmout;
                order.VoucherValue = hoaDon.VoucherValue ?? 0;
                order.TotalAmoutAfterApplyingVoucher = hoaDon.TotalAmoutAfterApplyingVoucher;
                order.ShippingFee = hoaDon.ShippingFee;
                order.Create_Date = DateTime.Now;
                order.Description = hoaDon.Description;
                if (hoaDon.PaymentType == "COD")
                {
                    order.OrderStatusId = Guid.Parse("1C54C2DD-2FA5-4041-9B94-FB613BEBDFBC"); // chờ xác nhận
                }
                else
                {
                    order.OrderStatusId = Guid.Parse("9C54C2DD-2FA5-4041-9B94-FB613BEBDFBC"); // chờ thanh toán
                    order.Payment_Date = DateTime.Now;
                    
                    
                }
                var response = await _httpClient.PostAsJsonAsync<OrderVM>("https://localhost:7007/api/order/create", order); // tạo order
                if (response.IsSuccessStatusCode)// nếu done
                {
                    _orderId = order.Id;
                    // get cart
                    var MyCart = await _httpClient.GetFromJsonAsync<Cart>($"https://localhost:7007/api/cart/get-item-Cart?userId={userId}");
                    // get cartitem
                    var myCartId = MyCart.Id;
                    var GetItemMyCart = await _httpClient.GetFromJsonAsync<List<CartItem>>($"https://localhost:7007/api/cartitem/getcartitembycartid?cartid={myCartId}");
                    foreach (var item in GetItemMyCart)
                    {
                        var orderItem = new OrderItemVM()// tạo order item
                        {
                            Id = Guid.NewGuid(),
                            OrderId = order.Id,
                            ProductDetailId = item.ProductDetail_ID,
                            Quantity = item.Quantity,
                            Price = item.Price
                        };
                        var responsePostCart = await _httpClient.PostAsJsonAsync<OrderItemVM>("https://localhost:7007/api/orderitem/create", orderItem); // tạo order
                        var responseDeleteCartItem = await _httpClient.DeleteAsync($"https://localhost:7007/api/cartitem/Delete-CartItem?cartId={myCartId}");// xoá sp trong cart
                        var responseUpdateQuantityProductDetail = await _httpClient.GetAsync($"https://localhost:7007/api/productDetail/UpdateQuantityById?productDetailId={item.ProductDetail_ID}&quantity={item.Quantity}");// update lại sp
                    }
                    if (hoaDon.PaymentType == "COD")
                    {
                        return "url_thanhtoancod";
                    }
                    else
                    {
                        // tạo bill vào xoá giỏ hàng .... Bill có trạng thái là chờ thanh toán

                        string vnp_Returnurl = "https://localhost:7095/Bill/PaymentCallBack"; //URL nhan ket qua tra ve 
                        string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; //URL thanh toan cua VNPAY 
                        string vnp_TmnCode = "P4VW9FD1"; //Ma định danh merchant kết nối (Terminal Id)
                        string vnp_HashSecret = "OPHRXNCKQAUVHIJNWXXTMPPYBVPAXUTF"; //Secret Key
                        string ipAddr = HttpContext.Connection.RemoteIpAddress?.ToString();
                        //Get payment input
                        //Save order to db

                        //Build URL for VNPAY
                        VnPayLibrary vnpay = new VnPayLibrary();
                        vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
                        vnpay.AddRequestData("vnp_Command", "pay");
                        vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
                        vnpay.AddRequestData("vnp_Amount", (order.TotalAmout * 100).ToString());
                        vnpay.AddRequestData("vnp_CreateDate", order.Create_Date?.ToString("yyyyMMddHHmmss"));
                        vnpay.AddRequestData("vnp_CurrCode", "VND");
                        vnpay.AddRequestData("vnp_IpAddr", ipAddr);
                        vnpay.AddRequestData("vnp_Locale", "vn");
                        vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OrderCode);
                        vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

                        vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
                        vnpay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

                        //Add Params of 2.1.0 Version
                        //Billing

                        string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
                        //log.InfoFormat("VNPAY URL: {0}", paymentUrl);

                        return paymentUrl;
                    }

                }

                return "";
            }

            catch
            {
                return "";
            }

        }
        [HttpGet]
        public async Task<IActionResult> PaymentCallBackAsync()
        {
            try
            {
                if (Request.Query.Count > 0)
                {
                    string vnp_HashSecret = "OPHRXNCKQAUVHIJNWXXTMPPYBVPAXUTF"; //Chuoi bi mat
                    var vnpayData = Request.Query;
                    VnPayLibrary vnpay = new VnPayLibrary();

                    foreach (var s in vnpayData)
                    {
                        //get all querystring data
                        if (!string.IsNullOrEmpty(s.Key) && s.Key.StartsWith("vnp_"))
                        {
                            vnpay.AddResponseData(s.Key, vnpayData[s.Key]);
                        }
                    }
                    //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                    //vnp_TransactionNo: Ma GD tai he thong VNPAY
                    //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                    //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                    long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
                    long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                    string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                    string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                    String vnp_SecureHash = Request.Query["vnp_SecureHash"];
                    String TerminalID = Request.Query["vnp_TmnCode"];
                    long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                    String bankCode = Request.Query["vnp_BankCode"];

                    bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                    if (checkSignature)
                    {
                        if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                        {
                            Guid DaThanhToanStatus= Guid.Parse("2C54C2DD-2FA5-4041-9B94-FB613BEBDFBC");// đã thanh toán
                            // change status
                            var responses = await _httpClient.GetAsync($"https://localhost:7007/api/order/updatestatus?OrderId={_orderId}&StatusId={DaThanhToanStatus}");
                            if (responses.IsSuccessStatusCode)
                            {
                                return RedirectToAction("CheckOutSuccess");
                            }
                        }
                    }
                    return BadRequest();
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }


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
