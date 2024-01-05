using AppData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AppData.Configurations
{
    public class LichSuTichDiemConfiguration : IEntityTypeConfiguration<LichSuTichDiem>
    {
        public void Configure(EntityTypeBuilder<LichSuTichDiem> builder)
        {
            builder.ToTable("LichSuMuaHang");
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.KhachHang).WithMany(x => x.LichSuTichDiems).HasForeignKey(x => x.IDKhachHang);
            builder.HasOne(x => x.HoaDon).WithMany(x => x.LichSuTichDiems).HasForeignKey(x => x.IDHoaDon);
        }
    }
}
