using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.Models
{
    public class VoucherDetails
    {
        [Key]
        public Guid Id { get; set; }
        public Guid VoucherId { get; set; }
        public Guid ProductDetailId { get; set; }
        public int? Value { get; set; }
        [ForeignKey("VoucherId")]
        public Voucher? Voucher { get; set; }

        [ForeignKey("ProductDetailId")]
        public ProductDetail? productDetail { get; set; }
    }
}
