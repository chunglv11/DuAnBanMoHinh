using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.Models
{
    public class WishList
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductDetailId { get; set; }
        [ForeignKey("UserId")]
        public User? user { get; set; }

        [ForeignKey("ProductDetailId")]
        public ProductDetail? productDetail { get; set; }
    }
}
