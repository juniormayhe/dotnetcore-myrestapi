using System.Threading.Tasks;

namespace RestTestWebApp.Services
{
    public interface IEmailService
    {
        Task SendEmail(string emailTo, string subject, string message);
    }
}