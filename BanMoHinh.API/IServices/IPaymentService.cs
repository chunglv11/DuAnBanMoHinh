using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.API.IServices
{
    public interface IPaymentService
    {
        public Task<bool> Create(Payment payment);

        public Task<bool> Delete(Guid id);

        public Task<List<Payment>> GetAll();

        public Task<Payment> GetItem(Guid id);

        public Task<bool> Update(Guid id, Payment item);
    }
}
