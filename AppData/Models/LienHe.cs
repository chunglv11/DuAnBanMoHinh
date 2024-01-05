using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class LienHe
    {
        public Guid Id { get; set; }

        public string HoTen { get; set; }

        public string Email { get; set; }

        public DateTime NgayGui { get; set; }

        public string? NoiDung { get; set; }

        public string? Sdt { get; set; }
    }
}
