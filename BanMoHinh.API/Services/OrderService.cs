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
    }
}
