using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using Microsoft.EntityFrameworkCore;

namespace BanMoHinh.API.Services
{
    public class VoucherStatusService : IVoucherStatusService
    {
        public MyDbContext _dbContext;

        public VoucherStatusService(MyDbContext myDbContext)
        {
            _dbContext = myDbContext;
        }
        public async Task<List<VoucherStatus>> GetAll()
        {
            return await _dbContext.voucherstatus.ToListAsync();
        }
    }
}
