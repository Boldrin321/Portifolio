using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Campaign
    {
        public int IdCampaign { get; set; }
        public string Name { get; set; }
        public string EmailFrom { get; set; }
        public string EmailFromName { get; set; }
        public string SubjectEmail { get; set; }
        public int TemplateId { get; set; }

    }
}
