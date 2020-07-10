using API.Models;
using RestSharp;
using SendGrid;
using System.Threading.Tasks;

namespace API.MailService
{
    public interface IMailService
    {
        public Task<System.Net.HttpStatusCode> Send(Lead lead, Campaign campaign, string _sendgrid);
    }
}
