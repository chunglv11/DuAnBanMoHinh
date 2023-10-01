using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using Microsoft.EntityFrameworkCore;

namespace BanMoHinh.API.Services
{
    public class VoucherProductService : IVoucherProductService
    {
        private readonly MyDbContext _dbContext;

        public VoucherProductService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Create(VoucherProduct item)
        {
            try
            {
                var voucherproduct = new VoucherProduct()
                {
                    VoucherId = item.VoucherId,
                    ProductId = item.ProductId,
                };
                await _dbContext.VoucherDetails.AddAsync(voucherproduct);
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
                var item = await _dbContext.VoucherDetails.FirstOrDefaultAsync(c => c.Id == id);
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

        public async Task<List<VoucherProduct>> GetAll()
        {
            return await _dbContext.VoucherDetails.ToListAsync();
        }

        public async Task<VoucherProduct> GetItem(Guid id)
        {
            return await _dbContext.VoucherDetails.FindAsync(id);
        }

        public async Task<bool> Update(Guid id, VoucherProduct rank)
        {
            try
            {
                var voucherproduct = await _dbContext.VoucherDetails.FirstOrDefaultAsync(c => c.Id == id);

                voucherproduct.VoucherId = rank.VoucherId;
                voucherproduct.ProductId = rank.ProductId;
            

                _dbContext.VoucherDetails.Update(voucherproduct);
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
