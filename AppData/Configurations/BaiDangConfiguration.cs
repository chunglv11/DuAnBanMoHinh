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
    internal class BaiDangConfiguration : IEntityTypeConfiguration<BaiDang>
    {
        public void Configure(EntityTypeBuilder<BaiDang> builder)
        {
            builder.ToTable("BaiDang");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TieuDe).IsRequired();
            builder.Property(x => x.AnhDaiDien).IsRequired();
            builder.Property(x => x.NoiDung).HasColumnType("nvarchar(MAX)").IsRequired();
            builder.Property(x => x.MoTa).HasColumnType("nvarchar(200)");
            builder.Property(x => x.NgayDang).HasColumnType("datetime");
            builder.Property(x => x.NgayCapNhat).HasColumnType("datetime");
            builder.Property(x => x.TrangThai);
            builder.HasOne(x => x.nhanVien).WithMany(x => x.BaiDangs).HasForeignKey(x => x.IdNV);
        }
    }
}
