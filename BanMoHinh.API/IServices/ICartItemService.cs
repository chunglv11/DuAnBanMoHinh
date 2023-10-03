using BanMoHinh.Share.Models;

namespace BanMoHinh.API.IServices
{
    public interface ICartItemService
    {
        public Task<bool> AddCartItem(CartItem item);

        // Read (Lấy danh sách các mục trong CartItem bởi CartId)
        public Task<bool> GetCartItemsByCartId(Guid cartItemId);

        // Update (Cập nhật thông tin một mục trong CartItem)
        public Task<bool> UpdateCartItem(Guid cartItemId, int newquantity, int newPrice);

        // Delete (Xóa một mục trong CartItem)
        public Task<bool> DeleteCartItem(Guid cartItemId, Guid productdetailId, Guid CartId);
    }
}
