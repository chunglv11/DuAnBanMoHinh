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
        public Guid? VoucherTypeId { get; set; }
        public Guid? VoucherStatusId { get; set; }
        public string? Code { get; set; }
        public int? Quantity { get; set; }
        public int? Value { get; set; }
        //thêm hình thức giảm
        public int? Minimum_order_value { get; set; }
        public DateTime? Create_Date { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public bool? Status { get; set; }
        [ForeignKey("VoucherTypeId")]
        public VoucherType? VoucherType { get; set; }
        [ForeignKey("VoucherStatusId")]
        public VoucherStatus? VoucherStatus { get; set; }
        
        public virtual List<Order>? Orders { get; set; }
    }
}
