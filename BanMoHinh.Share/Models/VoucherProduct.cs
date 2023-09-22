using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.Models
{
    public class VoucherProduct
    {
        [Key]
        public Guid Id { get; set; }
        public Guid VoucherId { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("VoucherId")]
        public Voucher? Voucher { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
