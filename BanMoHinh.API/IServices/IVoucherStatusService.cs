using BanMoHinh.Share.Models;

namespace BanMoHinh.API.IServices
{
    public interface IVoucherStatusService
    {
        public Task<List<VoucherStatus>> GetAll();
    }
}
