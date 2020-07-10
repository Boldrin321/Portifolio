using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MauticApi.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public int ID_Mautic { get; set; }
        public int SegmentId { get; set; }
        public string Name { get; set; }
        //public string lastname { get; set; }
        public string Email { get; set; }
        public string IpAddress { get; set; }
    }
}
