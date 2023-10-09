﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.ViewModels
{
    public class ProductDetailVM
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? SizeId { get; set; }
        public Guid? ColorId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceSale { get; set; }
        public DateTime? Create_At { get; set; }
        public DateTime? Update_At { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
    }
}