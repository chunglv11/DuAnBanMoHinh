using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.Models
{
    public class Voucher
    {
        [Key]
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public DateTime? Create_Date { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public int? Quantity { get; set; }
        [ForeignKey("VoucherTypeId")]
        public VoucherType? voucherType { get; set; }
    }
}
