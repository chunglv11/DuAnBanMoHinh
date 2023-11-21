using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.Client.IServices
{
    public interface IproductDetailApiClient
    {
        Task<bool> CreateProduct(ProductDetailVM request, Guid productId, Guid sizeId, Guid colorId, string edit);
        Task<bool> UpdateProduct(ProductDetailVM request, Guid sizeId, Guid colorId, string edit);
        Task<bool> DeleteProductDetail(Guid request);
        Task<ProductDetailVM> GetByIdProductDetail(Guid productDetailId);
        Task<List<ProductDetailVM>> GetAllProductDetail();
        Task<List<ProductVM>> GetListProduct();
        Task<List<Colors>> GetListColor();
        Task<List<SizeVM>> GetListSize();
    }
}
