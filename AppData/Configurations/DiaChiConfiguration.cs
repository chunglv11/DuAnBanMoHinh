using AppData.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Configurations
{
    
    internal class DiaChiConfiguration : IEntityTypeConfiguration<DiaChi>
    {
        public void Configure(EntityTypeBuilder<DiaChi> builder)
        {
            builder.ToTable("DiaChi");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Ten).IsRequired();
            builder.Property(x => x.Sdt).IsRequired();
            builder.Property(x => x.ThanhPho).IsRequired();
            builder.Property(x => x.Huyen).IsRequired();
            builder.Property(x => x.Xa).IsRequired();
            builder.Property(x => x.DiaChiChiTiet).IsRequired();
            builder.Property(x => x.IsDefault);
            builder.HasOne(x => x.KhachHang).WithMany(x => x.DiaChis).HasForeignKey(x => x.IdKhachHang);
        }
    }
}
