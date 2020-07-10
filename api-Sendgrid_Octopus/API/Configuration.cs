using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public static class Configuration
    {
        public static string sendgrid { get { return GetConfiguration()["Variables:Sendgridkey"]; } }
        public static string octopusKey { get { return GetConfiguration()["Variables:Octopuskey"]; } }
        public static string octopusUrl { get { return GetConfiguration()["Variables:OctopusUrlFull"]; } }
        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json");
            return builder.Build();    
        }
    }
}
