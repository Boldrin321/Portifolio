using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MauticSendEmail.Configuration
{
    public class Configuration
    {
        public static string MauticAutorization { get { return GetConfiguration()["Variables:MauticAutorization"]; } }
        public static string MauticCookie { get { return GetConfiguration()["Variables:MauticCookie"]; } }
        public static string Ip { get { return GetConfiguration()["Variables:Ip"]; } }
        public static string GetSegment { get { return GetConfiguration()["Variables:ContactNotification"]; } }
        public static string Limit { get { return GetConfiguration()["Variables:Limit"]; } }
        public static string ContactNotification { get { return GetConfiguration()["Variables:ContactNotification"]; } }
        public static string Start { get { return GetConfiguration()["Variables:StartNotification"]; } }
        public static string End { get { return GetConfiguration()["Variables:EndNotification"]; } }
        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json");
            return builder.Build();
        }
    }
}
