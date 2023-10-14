using BanMoHinh.Share.Models;

namespace BanMoHinh.API.IServices
{
    public interface IVoucherTypeServices
    {
        public Task<List<VoucherType>> GetAll();
    }
}
