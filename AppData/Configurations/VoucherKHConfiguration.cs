using AppData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Configurations
{
    public class VoucherKHConfiguration : IEntityTypeConfiguration<VoucherKH>
    {
        public void Configure(EntityTypeBuilder<VoucherKH> builder)
        {
            builder.ToTable("VoucherKH");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.KhachHang).WithMany(x => x.VoucherKHs).HasForeignKey(x => x.IDKhachHang);
            builder.HasOne(x => x.Voucher).WithMany(x => x.VoucherKHs).HasForeignKey(x => x.IDVoucher);
        }
    }
}
