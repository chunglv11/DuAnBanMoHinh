using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.Models
{
    public class Adress
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string? Province { get; set; } // Tỉnh/Thành phố
        public string? District { get; set; } // Quận/Huyện
        public string? Ward { get; set; } // Xã
        //thêm thược tính địa chỉ chi tiết và isdefault

        [ForeignKey("UserId")]
        public User? User{ get; set; }
    }
}