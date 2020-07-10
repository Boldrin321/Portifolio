using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot_Update.Model
{
    class HubSpotModel
    {
        public int Id { get; set; }
        public string BusinessType { get; set; }

        public HubSpotModel( int id, string businesstype)
        {
            this.Id = id;
            this.BusinessType = businesstype;
        }
    }
}
