using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<DonMuaChiTietVM>> getAllDonMuaChiTiet(Guid idHoaDon)
        {
            var lstDonMuaCT = await(from a in _dbContext.OrderItem
                                    where a.OrderId == idHoaDon
                                    join b in _dbContext.Order on a.OrderId equals b.Id
                                    //join c in _dbContext.Rate on a.Id equals c.Id //ko hiểu sao thêm cái này lại trả về []
                                    join d in _dbContext.ProductDetail on a.ProductDetailId equals d.Id
                                    join e in _dbContext.Size on d.SizeId equals e.Id
                                    join f in _dbContext.Colors on d.ColorId equals f.ColorId
                                    join g in _dbContext.Product on d.ProductId equals g.Id
                                    select new DonMuaChiTietVM()
                                    {
                                        ID = b.Id,
                                        NgayTao = b.Create_Date,
                                        NgayThanhToan = b.Payment_Date,
                                        TenNguoiNhan = b.RecipientName,
                                        SDT = b.RecipientPhone,
                                        DiaChi = b.RecipientAddress,
                                        TienShip = b.ShippingFee,
                                        TrangThaiGiaoHang = b.OrderStatusId,
                                        PhuongThucThanhToan = b.PaymentType,
                                        IDCTHD = a.Id,
                                        DonGia = a.Price,
                                        SoLuong = a.Quantity,
                                        TenKichCo = e.SizeName,
                                        TenMau = f.ColorName,
                                        TenSanPham = g.ProductName,
                                        DuongDan = _dbContext.ProductImage.First(c => c.ProductDetailId == d.Id).ImageUrl,
                                        HinhThucGiamGia = b.VoucherId == null ? null : (_dbContext.Voucher.FirstOrDefault(c => c.Id == b.VoucherId)).Discount_Type,
                                        GiaTriVC = b.VoucherId == null ? null : (_dbContext.Voucher.FirstOrDefault(c => c.Id == b.VoucherId)).Value,
                                        

                                    }).ToListAsync();
            return lstDonMuaCT;
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

        public async Task<QLHDViewModel> GetQLHDWithDetails(Guid orderId)
        {
            var qlhdWithDetails = await (from od in _dbContext.Order
                                         join ods in _dbContext.OrderStatus on od.OrderStatusId equals ods.Id
                                         where od.Id == orderId
                                         select new QLHDViewModel
                                         {
                                             Id = od.Id,
                                             OrderStatusId = od.OrderStatusId,
                                             OrderStatusName = ods.OrderStatusName,
                                             PaymentType = od.PaymentType,
                                             OrderCode = od.OrderCode,
                                             RecipientName = od.RecipientName,
                                             RecipientAddress = od.RecipientAddress,
                                             RecipientPhone = od.RecipientPhone,
                                             TotalAmout = od.TotalAmout,
                                             VoucherValue = od.VoucherValue,
                                             TotalAmoutAfterApplyingVoucher = od.TotalAmoutAfterApplyingVoucher,
                                             ShippingFee = od.ShippingFee,
                                             Create_Date = od.Create_Date,
                                             Ship_Date = od.Ship_Date,
                                             Payment_Date = od.Payment_Date,
                                             Delivery_Date = od.Delivery_Date,
                                             Description = od.Description,
                                             OrderItem = (from odi in _dbContext.OrderItem
                                                          join prd in _dbContext.ProductDetail on odi.ProductDetailId equals prd.Id
                                                          join pr in _dbContext.Product on prd.ProductId equals pr.Id
                                                          join cl in _dbContext.Colors on prd.ColorId equals cl.ColorId
                                                          join sz in _dbContext.Size on prd.SizeId equals sz.Id
                                                          where odi.OrderId == od.Id
                                                          select new OrderItemCTViewModel
                                                          {
                                                              Id = odi.Id,
                                                              OrderId = od.Id,
                                                              ProductDetailId = prd.Id,
                                                              ProductName = pr.ProductName,
                                                              ProductId = pr.Id,
                                                              SizeId = sz.Id,
                                                              SizeName = sz.SizeName,
                                                              ColorId = cl.ColorId,
                                                              ColorName = cl.ColorName,
                                                              Quantity = odi.Quantity??0,
                                                              PriceSale = odi.Price??0,
                                                              ProductImages = (from pi in _dbContext.ProductImage
                                                                               where pi.ProductDetailId == prd.Id
                                                                               select pi.ImageUrl).ToList()
                                                          }).ToList()
                                         }).FirstOrDefaultAsync();

            return qlhdWithDetails;
        }

        public Task<bool> HuyDon(Guid OrderId, Guid StatusId)
        {
            throw new NotImplementedException();
        }
    }
}
