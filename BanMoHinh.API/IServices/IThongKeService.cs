using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.API.IServices
{
    public interface IThongKeService
    {
       Task< ThongKeViewModel> ThongKe(string startDate, string endDate);
    }
}
