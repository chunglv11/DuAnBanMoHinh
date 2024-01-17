namespace BanMoHinh.Client.IServices
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage);

    }
}
