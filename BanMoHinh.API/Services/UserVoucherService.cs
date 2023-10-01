using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using Microsoft.EntityFrameworkCore;

namespace BanMoHinh.API.Services
{
    public class UserVoucherService : IUserVoucherService
    {
        private readonly MyDbContext _dbContext;

        public UserVoucherService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Create(UserVoucher item)
        {
            try
            {
                var uservoucher = new UserVoucher()
                {
                    UserId = item.UserId,
                    VoucherId = item.VoucherId,
                    Status = item.Status
                 
                };
                await _dbContext.VoucherUser.AddAsync(uservoucher);
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
                var item = await _dbContext.VoucherUser.FirstOrDefaultAsync(c => c.Id == id);
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

        public async Task<List<UserVoucher>> GetAll()
        {
            return await _dbContext.VoucherUser.ToListAsync();
        }

        public async Task<UserVoucher> GetItem(Guid id)
        {
            return await _dbContext.VoucherUser.FindAsync(id);
        }

        public async Task<bool> Update(Guid id, UserVoucher userVoucher)
        {
            try
            {
                var rates = await _dbContext.VoucherUser.FirstOrDefaultAsync(c => c.Id == id);

                rates.UserId = userVoucher.UserId;
                rates.VoucherId = userVoucher.VoucherId;
                rates.Status = userVoucher.Status;
              

                _dbContext.VoucherUser.Update(rates);
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
