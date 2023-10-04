using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BanMoHinh.API.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly MyDbContext _dbContext;
        public OrderItemService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Create(OrderItemVM item)
        {
            try
            {
                var orderitem = new OrderItem()
                {
                    OrderId = item.OrderId,
                    ProductDetailId = item.ProductDetailId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    
                };
                await _dbContext.OrderItem.AddAsync(orderitem);
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
                var item = await _dbContext.OrderItem.FirstOrDefaultAsync(c => c.Id == id);
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

        public async Task<List<OrderItem>> GetAll()
        {
            return await _dbContext.OrderItem.ToListAsync();

        }

        public async Task<OrderItem> GetItem(Guid id)
        {
            return await _dbContext.OrderItem.FindAsync(id);

        }

        public async Task<bool> Update(Guid id, OrderItemVM item)
        {
            try
            {
                var orderitem = await _dbContext.OrderItem.FirstOrDefaultAsync(c => c.Id == id);

                orderitem.OrderId = item.OrderId;
                orderitem.ProductDetailId = item.OrderId;
                orderitem.Quantity = item.Quantity;
                orderitem.Price = item.Price;
               

                _dbContext.OrderItem.Update(orderitem);
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
