using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.ViewModels
{
    public class ProductVM
    {
        public Guid Id { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? MaterialId { get; set; }
        public string? ProductName { get; set; }
        public int? AvailableQuantity { get; set; }
        public DateTime? Create_At { get; set; }
        public DateTime? Update_At { get; set; }
        public string? Description { get; set; }
        public string? Long_Description { get; set; }
        public bool? Status { get; set; }
    }
}
