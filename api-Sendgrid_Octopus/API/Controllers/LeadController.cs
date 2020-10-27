using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Models;
using API.MailService;
using System.Linq;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using API.Octopus;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly IMailService _mailService;
        private readonly IMailList _mailList;
        private string _OctopusKey;
        private string _OctopusUrlFull;
        private string _sendgrid;
        private DateTime teste;
        public LeadsController(EFContext context, IMailService mailService, IMailList mailList)
        {
            _context = context;
            _mailService = mailService;
            _mailList = mailList;
            teste = DateTime.Now;
        }

        [HttpPost]
        public JsonResult PostLead(Lead lead)
        {
            _context.Lead.Add(lead);


            var campaign = _context.Campaign
                                        //.Where<Campaign>(c => c.Name == lead.CampaignName)
                                        //.Single();

            var configuration = Configuration.GetConfiguration();
            _OctopusKey = configuration["Variables:Octopuskey"];
            _OctopusUrlFull = configuration["Variables:OctopusUrlFull"];
            _sendgrid = configuration["Variables:Sendgridkey"];

            var response1 = _mailList.AddInList(lead, _OctopusKey, _OctopusUrlFull);
            var response2 = _mailService.Send(lead, campaign, _sendgrid);
            _context.SaveChanges();

            return new JsonResult(new { success = true, OctopusResult = response1, SendGridResult = response2,operationTime = teste });
        }

        [HttpGet]
        public JsonResult GetJson()
        {
            return new JsonResult(new { testtime = teste });
        }
    }

}
