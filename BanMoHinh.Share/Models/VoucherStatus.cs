using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.Models
{
    public class VoucherStatus
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public virtual List<Voucher> Vouchers { get; set; }
    }
}
