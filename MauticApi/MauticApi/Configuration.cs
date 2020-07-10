using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MauticApi
{
    public class Configuration
    {
        public static string Token { get { return GetConfiguration()["Variables:AcessToken"]; } }
        public static string MauticAutorization { get { return GetConfiguration()["Variables:MauticAutorization"]; } }
        public static string MauticCookie { get { return GetConfiguration()["Variables:MauticCookie"]; } }
        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json");
            return builder.Build();
        }
    }
}
