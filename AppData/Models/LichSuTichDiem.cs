namespace AppData.Models
{
    public class LichSuTichDiem
    {
        public Guid ID { get; set; }
        public Guid? IDKhachHang { get; set; }
        public Guid IDHoaDon { get; set; }
        public virtual KhachHang? KhachHang { get; set; }
        public virtual HoaDon? HoaDon { get; set; }
    }
}
