using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using Microsoft.EntityFrameworkCore;

namespace BanMoHinh.API.Services
{
    public class VoucherTypeServices : IVoucherTypeServices
    {
        public MyDbContext _dbContext;
        public VoucherTypeServices(MyDbContext myDbContext)
        {
            _dbContext = myDbContext;
        }
        public async Task<List<VoucherType>> GetAll()
        {
            return await _dbContext.VoucherType.ToListAsync();
        }
    }
}
