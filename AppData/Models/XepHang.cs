using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class XepHang
    {
        public Guid Id { get; set; }
        public string? Ten { get; set; }
        public int? DiemMin { get; set; }
        public int? DiemMax { get; set; }
        public string? Mota { get; set; }
        public virtual List<KhachHang>? KhachHangs { get; set; }
    }
}
