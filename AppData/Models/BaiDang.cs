using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class BaiDang
    {
        public Guid Id { get; set; }
        public Guid? IdNV { get; set; }
        public string? TieuDe { get; set; }
        public string? AnhDaiDien { get; set; }
        public string? NoiDung { get; set; }
        public DateTime? NgayDang { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public string? MoTa { get; set; }
        public bool? TrangThai { get; set; }
        public virtual NhanVien? nhanVien { get; set; }
    }
}
