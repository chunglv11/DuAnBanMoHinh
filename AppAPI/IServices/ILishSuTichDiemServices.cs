using AppData.Models;
using AppData.ViewModels;

namespace AppAPI.IServices
{
    public interface ILishSuTichDiemServices
    {
        public bool Add(Guid IdKhachHang, Guid IdHoaDon);


        public bool Update(Guid Id, Guid IdKhachHang, Guid IdHoaDon);

		public bool Delete(Guid Id);
        public LichSuTichDiem GetById(Guid Id);
        public List<LichSuTichDiem> GetAll();
        public Task<List<DonMuaViewModel>> getAllDonMua(Guid idKhachHang);
        public Task<List<DonMuaChiTietViewModel>> getAllDonMuaChiTiet(Guid idHoaDon);
        public Task<ChiTietHoaDonDanhGiaViewModel> getCTHDDanhGia (Guid idcthd);
    }
}
