using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using Microsoft.EntityFrameworkCore;

namespace BanMoHinh.API.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly MyDbContext _dbContext;
        public PaymentService(MyDbContext myDbContext)
        {
            _dbContext = myDbContext;
        }
        public async Task<bool> Create(Payment payment)
        {
            try
            {
                var payment1 = new Payment()
                {
                    PaymentName = payment.PaymentName,
                    Id = payment.Id,
                };
                await _dbContext.Payment.AddAsync(payment1);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Payment>> GetAll()
        {
            return await _dbContext.Payment.ToListAsync();
        }

        public Task<Payment> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Guid id, Payment item)
        {
            throw new NotImplementedException();
        }
    }
}
