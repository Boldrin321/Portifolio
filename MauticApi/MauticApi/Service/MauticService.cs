using MauticApi.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MauticApi.Service
{
    public class MauticService : IService
    {
        public IRestResponse CreateContact(Contact contact, string _mauticAutorization, string _mauticCookie)
        {
            //var client = new RestClient($"http://3.235.90.62/api/contacts/new?access_token={_mauticToken}");
            var client = new RestClient($"http://3.235.90.62/api/contacts/new");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"{_mauticAutorization}");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", $"{_mauticCookie}");
            var body = "{\n\t\"firstname\" : \""+contact.Name+"\",\n    \"lastname\"  : \""+""+"\",\n    \"email\"     : \""+contact.Email+"\",\n    \"ipAddress\" : \""+$"{contact.IpAddress}"+"\",\n    \"overwriteWithBlank\" : true\n}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var json = response.Content;
            JObject jObject = JObject.Parse(json);
            JToken jUser = jObject["contact"];
            string id = jUser["id"].ToString();
            contact.ID_Mautic = Int32.Parse(id);
            return response;
        }

        public IRestResponse AddContactInSegment(Contact contact, string _mauticAutorization, string _mauticCookie)
        {
            //var client = new RestClient($"http://3.235.90.62/api/segments/{contact.segmentId}/contact/{contact.id}/add?access_token={_mauticToken}");
            var client = new RestClient($"http://3.235.90.62/api/segments/{contact.SegmentId}/contact/{contact.ID_Mautic}/add");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"{_mauticAutorization}");
            request.AddHeader("Cookie", $"{_mauticCookie}");
            IRestResponse response = client.Execute(request);
            return response;
        }
    }
}
