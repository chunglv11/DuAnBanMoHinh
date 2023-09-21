using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.Models
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }
        public string? PaymentName { get; set; }
        public virtual List<Order>? Order { get; set; }
    }
}
