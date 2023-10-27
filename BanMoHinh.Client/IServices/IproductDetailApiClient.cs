using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.Client.IServices
{
    public interface IproductDetailApiClient
    {
        Task<bool> CreateProduct(ProductDetailVM request, Guid productId, Guid sizeId, Guid colorId, string edit);
        Task<List<ProductVM>> GetListProduct();
        Task<List<Colors>> GetListColor();
        Task<List<SizeVM>> GetListSize();
    }
}
