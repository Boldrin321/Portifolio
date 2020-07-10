using System;
using System.Collections.Generic;
using System.Text;

namespace MauticSendEmail.Models
{
    public class Contact
    {
        //public int Id { get; set; }
        public int Id { get; set; }
        //public int SegmentId { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public int Points { get; set; }
        public DateTime? Last_email_receive { get; set; }
    }
}
