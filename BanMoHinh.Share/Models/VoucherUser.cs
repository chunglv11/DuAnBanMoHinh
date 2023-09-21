using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.Models
{
    public class VoucherUser
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("UserId")]
        public User? user { get; set; }
        [ForeignKey("VoucherId")]
        public Voucher? voucher { get; set; }
        public int Status { get; set; }
    }
}
