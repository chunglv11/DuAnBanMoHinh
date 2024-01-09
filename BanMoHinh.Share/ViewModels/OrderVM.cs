﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.ViewModels
{
    public class OrderVM
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? OrderStatusId { get; set; }
        public string? PaymentType { get; set; }
        public Guid? VoucherId { get; set; }
        public string? OrderCode { get; set; } // mã bill
        public string? RecipientName { get; set; }
        public string? RecipientAddress { get; set; }
        public string? RecipientPhone { get; set; }
        public decimal? TotalAmout { get; set; } // tổng tiền trước khi áp dụng
        public decimal? VoucherValue { get; set; } // giá trị voucher
        public decimal? TotalAmoutAfterApplyingVoucher { get; set; } // giá sau khi áp voucher
        public decimal? ShippingFee { get; set; } // phí ship
        public DateTime? Create_Date { get; set; } // ngày tạo bill
        public DateTime? Ship_Date { get; set; } // ngày Ship
        public DateTime? Payment_Date { get; set; } // ngày thanh toán
        public DateTime? Delivery_Date { get; set; } // ngày nhận hàng
        public string? Description { get; set; }
    }
}
