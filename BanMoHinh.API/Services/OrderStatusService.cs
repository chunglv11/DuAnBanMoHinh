using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BanMoHinh.API.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly MyDbContext _dbContext;
        public OrderStatusService(MyDbContext myDbContext)
        {
            _dbContext = myDbContext;
        }
        public async Task<bool> Create(OrderStatusVM item)
        {
            try
            {
                var orders = new OrderStatus()
                {
                    OrderStatusName = item.OrderStatusName,
                    Id = item.Id,
                };
                await _dbContext.OrderStatus.AddAsync(orders);
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
                var item = await _dbContext.OrderStatus.FirstOrDefaultAsync(c => c.Id == id);
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

        public async Task<List<OrderStatus>> GetAll()
        {
            return await _dbContext.OrderStatus.ToListAsync();
        }

        public Task<OrderStatus> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Guid id, OrderStatusVM item)
        {
            throw new NotImplementedException();
        }
    }
}
