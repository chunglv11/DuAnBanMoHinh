using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.API.IServices
{
    public interface IOrderItemService
    {
        public Task<bool> Create(OrderItemVM item);

        public Task<bool> Delete(Guid id);

        public Task<List<OrderItem>> GetAll();

        public Task<OrderItem> GetItem(Guid id);

        public Task<bool> Update(Guid id,  OrderItemVM item);
    }
}
