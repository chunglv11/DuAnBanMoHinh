using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BanMoHinh.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly MyDbContext _dbContext;
        public OrderService(MyDbContext myDbContext) {
            _dbContext = myDbContext;
        }
        public async Task<bool> Create(OrderVM item)
        {
            try
            {
                var order = new Order()
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    VoucherId = item.VoucherId,
                    PaymentType = item.PaymentType,
                    OrderStatusId = item.OrderStatusId,
                    OrderCode = item.OrderCode,
                    RecipientAddress = item.RecipientAddress,
                    RecipientPhone = item.RecipientPhone,
                    RecipientName = item.RecipientName,
                    TotalAmout  = item.TotalAmout,
                    TotalAmoutAfterApplyingVoucher = item.TotalAmoutAfterApplyingVoucher,
                    VoucherValue = item.VoucherValue,
                    Create_Date = item.Create_Date,
                    Payment_Date    = item.Payment_Date,
                    Delivery_Date = item.Delivery_Date,
                    Description = item.Description,
                    ShippingFee = item.ShippingFee,
                    Ship_Date = item.Ship_Date,
                   
                };
                await _dbContext.Order.AddAsync(order);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var item = await _dbContext.Order.FirstOrDefaultAsync(c => c.Id == id);
                _dbContext.Remove(item);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<List<Order>> GetAll()
        {
            return await _dbContext.Order.ToListAsync();

        }

        public async Task<Order> GetItem(Guid id)
        {
            return await _dbContext.Order.FindAsync(id);

        }

        public async Task<bool> Update(Guid id, Guid UserId, OrderVM item)
        {
            try
            {
                var order = await _dbContext.Order.FirstOrDefaultAsync(c => c.Id == id);

                order.UserId = item.UserId;
                order.VoucherId = item.VoucherId;
                order.OrderStatusId = item.OrderStatusId;
                order.OrderCode = item.OrderCode;
                order.RecipientAddress = item.RecipientAddress;
                order.RecipientPhone = item.RecipientPhone;
                order.RecipientName = item.RecipientName;
                order.TotalAmout = item.TotalAmout;
                order.TotalAmoutAfterApplyingVoucher = item.TotalAmoutAfterApplyingVoucher;
                order.VoucherValue = item.VoucherValue;
                order.Create_Date = item.Create_Date;
                order.Payment_Date = item.Payment_Date;
                order.Delivery_Date = item.Delivery_Date;
                order.Description = item.Description;
                order.ShippingFee = item.ShippingFee;
                order.Ship_Date = item.Ship_Date;
                _dbContext.Order.Update(order);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateStatus(Guid OrderId, Guid StatusId)
        {
            try
            {
                var order = await _dbContext.Order.FirstOrDefaultAsync(c => c.Id == OrderId);
                order.OrderStatusId = StatusId;
                _dbContext.Update(order);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateRank(int? point)
        {
            var ranks = _dbContext.Rank.ToList();
            if (ranks != null)
            {

            
            foreach (var item in ranks)
            {
                if (point >= item.PointsMin && point <= item.PoinsMax)
                {
                    var user = _dbContext.Users.FirstOrDefault(c => c.Points == point);
                    user.RankId = item.Id;
                    _dbContext.Users.Update(user);
                    _dbContext.SaveChanges();
                    return true;
                }
            }
        }
            return false;
        }
        public async Task<bool> UpdateTrangThaiGiaoHang(Guid idHoaDon, Guid idtrangThai, Guid? idNhanVien)
        {
            var update = await _dbContext.Order.FirstOrDefaultAsync(p => p.Id == idHoaDon);
            var chitiethoadon = await _dbContext.OrderItem.Where(p => p.OrderId == idHoaDon).ToListAsync();

            if (update != null)
            {
                if (idtrangThai == Guid.Parse("6C54C2DD-2FA5-4041-9B94-FB613BEBDFBC"))
                {
                    foreach (var item in chitiethoadon)
                    {
                        var CTsanPham = await _dbContext.ProductDetail.FirstOrDefaultAsync(p => p.Id == item.ProductDetailId);
                        CTsanPham.Quantity += item.Quantity;

                        var product = await _dbContext.Product.FindAsync(CTsanPham.ProductId);
                        if (product != null)
                        {
                            product.AvailableQuantity += item.Quantity;
                            _dbContext.Product.Update(product);
                        }

                        _dbContext.ProductDetail.Update(CTsanPham);
                    }
                }

                if (idtrangThai == Guid.Parse("4C54C2DD-2FA5-4041-9B94-FB613BEBDFBC"))
                {
                    var kh = await _dbContext.Users.FirstOrDefaultAsync(c => c.Id == idNhanVien);
                    var hoadon = await _dbContext.Order.FirstOrDefaultAsync(c => c.Id == idHoaDon);
                    if (kh != null && hoadon != null)
                    {
                        kh.Points += Convert.ToInt32(hoadon.TotalAmout);
                        UpdateRank(kh.Points);
                        _dbContext.Users.Update(kh);
                    }

                    update.Payment_Date ??= DateTime.Now;
                    update.Ship_Date ??= DateTime.Now;
                }

                update.OrderStatusId = idtrangThai;
                update.UserId = idNhanVien;

                try
                {
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    // Handle exception and possibly rollback changes
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> ThanhCong(Guid idHoaDon, Guid? idNhanVien) // Chỉ cho đơn online
        {
            try
            {
                var hd = _dbContext.Order.FirstOrDefault(c => c.Id == idHoaDon);
                hd.OrderStatusId = Guid.Parse("4C54C2DD-2FA5-4041-9B94-FB613BEBDFBC");
                hd.UserId = idNhanVien;
                hd.Ship_Date = DateTime.Now;
                hd.Payment_Date = DateTime.Now;
                _dbContext.Order.Update(hd);
                _dbContext.SaveChanges();
                //Cộng tích điểm cho khách
                var kh = await _dbContext.Users.FirstOrDefaultAsync(c => c.Id == idNhanVien);

                var hoadon = await _dbContext.Order.FirstOrDefaultAsync(c => c.Id == idHoaDon);
                if (kh != null && hoadon != null)
                {
                    kh.Points += Convert.ToInt32(hoadon.TotalAmout);
                    UpdateRank(kh.Points);
                    _dbContext.Users.Update(kh);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> HuyHD(Guid idhd, Guid idnv)
        {
            try
            {
                var hd = _dbContext.Order.Where(c => c.Id == idhd).FirstOrDefault();
                //Update hd
                hd.UserId = idnv;
                hd.OrderStatusId = Guid.Parse("6C54C2DD-2FA5-4041-9B94-FB613BEBDFBC");
                //hd.TongTien = 0;
                _dbContext.Order.Update(hd);
               await _dbContext.SaveChangesAsync();

                // Cộng lại số lượng hàng
                var chitiethoadon = await _dbContext.OrderItem.Where(p => p.OrderId == idhd).ToListAsync();


                if (chitiethoadon != null)
                {


                    foreach (var item in chitiethoadon)
                    {
                        var CTsanPham = await _dbContext.ProductDetail.FirstOrDefaultAsync(p => p.Id == item.ProductDetailId);
                        CTsanPham.Quantity += item.Quantity;

                        var product = await _dbContext.Product.FindAsync(CTsanPham.ProductId);
                        if (product != null)
                        {
                            product.AvailableQuantity += item.Quantity;
                            _dbContext.Product.Update(product);
                            await _dbContext.SaveChangesAsync();

                        }

                        _dbContext.ProductDetail.Update(CTsanPham);
                        await _dbContext.SaveChangesAsync();

                    }

                }
                    // Cộng lại số lượng voucher nếu áp dụng
                if (hd.VoucherId != null)
                {
                    var vc = await _dbContext.Voucher.FirstOrDefaultAsync(c => c.Id == hd.VoucherId);
                    var uservc = await  _dbContext.VoucherUser.FirstOrDefaultAsync(c => c.VoucherId == vc.Id && c.UserId == hd.UserId);
                    vc.Quantity += 1;
                    uservc.Status = true;
                    _dbContext.Voucher.Update(vc);
                    _dbContext.VoucherUser.Update(uservc);
                    await _dbContext.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateGhiChuHD(Guid idhd, Guid idnv, string ghichu)
        {
            try
            {
                var hd = _dbContext.Order.FirstOrDefault(c => c.Id == idhd);
                if (ghichu == "null")
                {
                    hd.Description = null;
                    hd.UserId = idnv;
                }
                else
                {
                    hd.Description = ghichu;
                }
                _dbContext.Order.Update(hd);
                _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
