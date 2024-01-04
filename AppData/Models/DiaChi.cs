using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class DiaChi
    {
        public Guid Id { get; set; }
        public Guid? IdKhachHang { get; set; }
        public string? Ten { get; set; }
        public string? Sdt { get; set; }
        public string? ThanhPho { get; set; }
        public string? Huyen { get; set; }
        public string? Xa { get; set; }
        public string? DiaChiChiTiet { get; set; }
        public bool? IsDefault { get; set; }
        public virtual KhachHang? KhachHang { get; set; }//thêm dịa chỉ, bài đăng,voucherkH,rank,
    }
}
