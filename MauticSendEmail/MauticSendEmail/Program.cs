using System;
using MauticSendEmail.Service;
using Newtonsoft.Json.Linq;
using MauticSendEmail.Models;
using System.Threading;

namespace MauticSendEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = Configuration.Configuration.GetConfiguration();
            string _mauticAutorization = configuration["Variables:MauticAutorization"];
            string _mauticCookie = configuration["Variables:MauticCookie"];
            string _ip = configuration["Variables:Ip"];
            string _segId = configuration["Variables:SegmentId"];
            string _contactNotification = configuration["Variables:ContactNotification"];
            string _startNotification = configuration["Variables:StartNotification"];
            string _endNotification = configuration["Variables:EndNotification"];
            var _limit = configuration["Variables:Limit"];

            Mautic mautic = new Mautic();
            Contact contact = new Contact();
            Email email = new Email();
            Segment segment = new Segment();

            var start = mautic.ApiStart(_mauticAutorization, _mauticCookie, _ip, _contactNotification, _startNotification);
            JObject jStart = JObject.Parse(start);
            var apiStart = jStart["success"].ToString();
            Console.WriteLine($"aviso que API começou: {apiStart}");



            var getSegment = mautic.GetSegment(_mauticAutorization, _mauticCookie, _ip, _segId);

            JObject jSegment = JObject.Parse(getSegment);
            JToken jUserSeg = jSegment["list"];
            segment.Id = Int32.Parse(jUserSeg["id"].ToString());
            segment.Name = jUserSeg["alias"].ToString();
            email.Id = Int32.Parse(jUserSeg["description"].ToString());

            var getTotal = mautic.GetContacts(_mauticAutorization, _mauticCookie, _ip, segment, _limit);
            JObject jGetTotal = JObject.Parse(getTotal);
            JToken jTotal = jGetTotal;
            var total = Int32.Parse(jTotal["total"].ToString());
            

            for (var cont = 1; cont <= total; cont++)
            {
                Console.WriteLine($"\nLoop: {cont}");
                var json = mautic.GetContacts(_mauticAutorization, _mauticCookie, _ip, segment, _limit);

                JObject tags2 = JObject.Parse(json);

                JObject objectContainer = tags2.Value<JObject>("contacts");

                foreach (var tag in objectContainer)
                {
                    contact.Id = Int32.Parse(tag.Key);

                    JObject jContact = JObject.Parse(json);

                    try
                    {
                        JToken jUser = jContact["contacts"][contact.Id.ToString()]["fields"]["all"];
                        contact.Email = jUser["email"].ToString();
                        contact.Firstname = jUser["firstname"].ToString();
                        contact.Points = Int32.Parse(jUser["points"].ToString());
                        contact.Last_email_receive = DateTime.Now;
                        Console.WriteLine($"\nContato: {contact.Firstname}");
                        var send = mautic.SendEmail(_mauticAutorization, _mauticCookie, _ip, contact, email);
                        JObject jSendResult = JObject.Parse(send);
                        var sendResult = jSendResult["success"].ToString();

                        if (sendResult == "True")
                        {
                            Console.WriteLine("\nEmail: enviado com sucesso");
                            var update = mautic.UpdateContact(_mauticAutorization, _mauticCookie, _ip, contact);
                            var removeOffSegment = mautic.RemoveContactSegment(_mauticAutorization, _mauticCookie, _ip, contact, segment);
                            Console.WriteLine($"Atualização do Contato: {update}");
                            Console.WriteLine($"Remoção do contato do segmento: {removeOffSegment}");
                        }
                        

                        contact.Id = 0;
                        contact.Firstname = "";
                        contact.Email = "";
                        contact.Points = 0;
                        contact.Last_email_receive = null;

                        Console.WriteLine("\nEsperando 60 segundos...");
                        Thread.Sleep(60000);
                    }
                    catch
                    {
                        Console.WriteLine("\nErro!!! ");
                    }
                }
            }

            var end = mautic.ApiEnd(_mauticAutorization, _mauticCookie, _ip, _contactNotification, _endNotification);
            JObject jEnd = JObject.Parse(end);
            var apiEnd = jEnd["success"].ToString();
            Console.WriteLine($"\naviso que API encerrou: {apiEnd}");
            Console.WriteLine("\n\nFim!!!");
            }
    }
}

