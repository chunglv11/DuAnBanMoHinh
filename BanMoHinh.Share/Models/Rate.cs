using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.Models
{
    public class Rate
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OrderItemId { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public int? Rating { get; set; }
        [ForeignKey("OrderItemId")]
        public OrderItem? OrderItem { get; set; }


    }
}
