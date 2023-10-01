using BanMoHinh.Share.Models;

namespace BanMoHinh.API.IServices
{
    public interface IVoucherProductService
    {
        public Task<bool> Create(VoucherProduct item);

        public Task<bool> Delete(Guid id);

        public Task<List<VoucherProduct>> GetAll();

        public Task<VoucherProduct> GetItem(Guid id);
        public Task<bool> Update(Guid id, VoucherProduct rank);
    }
}
