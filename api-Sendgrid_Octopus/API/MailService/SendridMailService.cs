using API.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace API.MailService
{
    public class SendridMailService : IMailService
    {
        public async Task<System.Net.HttpStatusCode> Send(Lead lead, Campaign campaign, string _sendgrid)
        {
            var apiKey = Environment.GetEnvironmentVariable(_sendgrid);
            var client = new SendGridClient(_sendgrid);
            var from = new EmailAddress(campaign.EmailFrom, campaign.EmailFromName);
            var subject = campaign.SubjectEmail;
            var to = new EmailAddress(lead.Email, lead.Name);
            var plainTextContent = "The campaign is: " + campaign.Name;
            var htmlContent = $"<strong>receita de {campaign.Name}!!!</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);            
            return response.StatusCode;
        }
    }
}
