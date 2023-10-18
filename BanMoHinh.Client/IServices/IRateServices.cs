using BanMoHinh.Share.Models;

namespace BanMoHinh.Client.IServices
{
    public interface IRateServices
    {
        public Task<bool> Create(Rate item);

        public Task<bool> Delete(Guid id,Guid orderid);

        public Task<List<Rate>> GetAll();

        public Task<Rate> GetItem(Guid id);
        public Task<List<Rate>> GetListRatebyorderId(Guid orderId);

        public Task<bool> Update(Guid id,Guid orderid, Rate rate);
    }
}
