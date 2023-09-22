﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? OrderStatusId { get; set; }
        public Guid? PaymentId { get; set; }
        public Guid? VoucherId { get; set; }
        public string? OrderCode { get; set; } // mã bill
        public string? RecipientName { get; set; }
        public string? RecipientAddress { get; set; }
        public string? RecipientPhone { get; set; }
        public int? TotalAmout { get; set; } // tổng tiền trước khi áp dụng
        public int? VoucherValue { get; set; } // giá trị voucher
        public int? TotalAmoutAfterApplyingVoucher { get; set; } // giá sau khi áp voucher
        public int? ShippingFee { get; set; } // phí ship
        public DateTime? Create_Date { get; set; } // ngày tạo bill
        public DateTime? Ship_Date { get; set; } // ngày Ship
        public DateTime? Payment_Date { get; set; } // ngày thanh toán
        public DateTime? Delivery_Date { get; set; } // ngày nhận hàng
        public string? Description { get; set; }


        [ForeignKey("UserId")]
        public User? User { get; set; }
        [ForeignKey("OrderStatusId")]
        public OrderStatus? OrderStatus { get; set; }
        [ForeignKey("PaymentId")]
        public Payment? PaymentType { get; set; }
        [ForeignKey("VoucherId")]
        public Voucher? Voucher { get; set; }

        public virtual List<OrderItem>? OrderItem { get; set; }
    }
}
