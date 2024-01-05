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
    internal class XepHangConfiguration : IEntityTypeConfiguration<XepHang>
    {
        public void Configure(EntityTypeBuilder<XepHang> builder)
        {
            builder.ToTable("XepHang");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Ten).HasColumnType("nvarchar(20)").IsRequired();
            builder.Property(x => x.Mota).HasColumnType("nvarchar(100)");
            builder.Property(x => x.DiemMin).HasColumnType("int").IsRequired();
            builder.Property(x => x.DiemMax).HasColumnType("int").IsRequired();
            builder.HasData(new XepHang() { Id = new Guid("491abc2c-3bfa-47dd-a55c-ed065295374c"), Ten = "Đồng",  DiemMin = 0,DiemMax = 0, Mota = "Thành viên"});
            builder.HasData(new XepHang() { Id = new Guid("c6c70bab-e95b-4e78-aaf1-4077e9508332"), Ten = "Bạc",  DiemMin = 1000000,DiemMax = 2000000, Mota = "rank bạc"});
            builder.HasData(new XepHang() { Id = new Guid("02a277fd-e8b4-42ae-b305-9d598fce3c80"), Ten = "Vàng",  DiemMin = 2000000, DiemMax = 4000000, Mota = "rank vàng"});
            builder.HasData(new XepHang() { Id = new Guid("376e1049-0e36-4b89-a240-b5eb8409f503"), Ten = "Kim Cương",  DiemMin = 4000000, DiemMax = 9000000, Mota = "rank kim cương"});
            
        }
    }
}
