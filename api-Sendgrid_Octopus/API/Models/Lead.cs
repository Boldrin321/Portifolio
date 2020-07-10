using System;

namespace API.Models
{
    public class Lead
    {
        public Lead()
        {
            Time = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string CampaignName { get; set; }
        public DateTime Time { get; set; }

    }
}
