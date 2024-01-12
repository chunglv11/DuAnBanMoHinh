using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.ViewModels
{
    public class DonMuaChiTietVM
    {
        public Guid ID { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public string? TenNguoiNhan { get; set; }
        public string? SDT { get; set; }
        public string? DiaChi { get; set; }
        public decimal? TienShip { get; set; }
        public string? PhuongThucThanhToan { get; set; }
        public Guid? TrangThaiGiaoHang { get; set; }
        public Guid IDCTHD { get; set; }
        public int? DonGia { get; set; }
        public int? SoLuong { get; set; }
        public string? TenKichCo { get; set; }
        public string? TenMau { get; set; }
        public string? TenSanPham { get; set; }
        public string? DuongDan { get; set; }
        public int? TrangThaiDanhGia { get; set; }
        public int? GiaTriVC { get; set; }
        public int? HinhThucGiamGia { get; set; }
    }
}
