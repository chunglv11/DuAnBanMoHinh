using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public Guid? CategoryId { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public Guid? BrandId { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public Guid? MaterialId { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string? ProductName { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 1")]
        public int? AvailableQuantity { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string? Description { get; set; }
        public DateTime? Create_At { get; set; }
        public DateTime? Update_At { get; set; }
        public string? Long_Description { get; set; }
        public int? Status { get; set; }


        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        [ForeignKey("BrandId")]
        public Brand? Brand { get; set; }

        public virtual List<ProductImage>? ProductImage { get; set; }
        public virtual List<ProductDetail>? ProductDetails { get; set; }
    }
}
