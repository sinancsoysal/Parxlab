using System.Threading.Tasks;

namespace Parxlab.Service.Contracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
