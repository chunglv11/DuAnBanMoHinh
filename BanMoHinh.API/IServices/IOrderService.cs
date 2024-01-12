using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.API.IServices
{
    public interface IOrderService
    {
        public Task<bool> Create(OrderVM item);

        public Task<bool> Delete(Guid id);
        public Task<bool> UpdateStatus(Guid OrderId,Guid StatusId);
        public Task<bool> HuyDon(Guid OrderId,Guid StatusId);
        public  Task<QLHDViewModel> GetQLHDWithDetails(Guid orderId);
        public Task<List<Order>> GetAll();

        public Task<Order> GetItem(Guid id);
        public Task<List<DonMuaChiTietVM>> getAllDonMuaChiTiet(Guid idHoaDon);

        public Task<bool> Update(Guid id, Guid UserId, OrderVM item);
    }
}
