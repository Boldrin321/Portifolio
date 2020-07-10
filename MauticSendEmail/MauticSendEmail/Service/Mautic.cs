using MauticSendEmail.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauticSendEmail.Service
{
    public class Mautic
    {
        public string GetSegment(string _mauticAutorization, string _mauticCookie, string _ip, string _segId)
        {
            var client = new RestClient($"{_ip}/api/segments/{_segId}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", _mauticAutorization);
            request.AddHeader("Cookie", _mauticCookie);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
        /*public string GetEmails(string _mauticAutorization, string _mauticCookie, string _ip, Email email)
        {
            var client = new RestClient($"{_ip}/api/emails/{email.Id}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader($"Authorization", _mauticAutorization);
            request.AddHeader($"Cookie", _mauticCookie);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }*/
        public string GetContacts(string _mauticAutorization, string _mauticCookie, string _ip, Segment segment, string _limit)
        {
            var client = new RestClient($"{_ip}/api/contacts?search=!email:null&orderBy=last_email_receive&orderByDir=asc&limit={_limit}&search=segment:{segment.Name}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", _mauticAutorization);
            request.AddHeader("Cookie", _mauticCookie);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public string SendEmail(string _mauticAutorization, string _mauticCookie, string _ip, Contact contact, Email email)
        {
            var client = new RestClient($"{_ip}/api/emails/{email.Id}/contact/{contact.Id}/send");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", _mauticAutorization);
            request.AddHeader("Cookie", _mauticCookie);
            IRestResponse response = client.Execute(request);
            return response.Content.ToString();
        }

        public string UpdateContact(string _mauticAutorization, string _mauticCookie, string _ip, Contact contact)
        {
            var client = new RestClient($"{_ip}/api/contacts/{contact.Id}/edit");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", _mauticAutorization);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", _mauticCookie);
            request.AddParameter("application/json", "{\n\t\"firstname\":\""+contact.Firstname+"\",\n\t\"email\":\""+contact.Email+"\",\n\t\"points\":\""+contact.Points+"\",\n\t\"last_email_receive\":\""+contact.Last_email_receive+"\"\t\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response.StatusCode.ToString();
        }

        public string RemoveContactSegment(string _mauticAutorization, string _mauticCookie, string _ip, Contact contact, Segment segment)
        {
            var client = new RestClient($"{_ip}/api/segments/{segment.Id}/contact/{contact.Id}/remove");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", _mauticAutorization);
            request.AddHeader("Cookie", _mauticCookie);
            IRestResponse response = client.Execute(request);
            return response.StatusCode.ToString();
        }

        public string ApiStart(string _mauticAutorization, string _mauticCookie, string _ip, string _contactNotification, string _startNotification)
        {
            var client = new RestClient($"{_ip}/api/emails/{_startNotification}/contact/{_contactNotification}/send");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", _mauticAutorization);
            request.AddHeader("Cookie", _mauticCookie);
            IRestResponse response = client.Execute(request);
            return response.Content.ToString();
        }

        public string ApiEnd(string _mauticAutorization, string _mauticCookie, string _ip, string _contactNotification, string _endNotification)
        {
            var client = new RestClient($"{_ip}/api/emails/{_endNotification}/contact/{_contactNotification}/send");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", _mauticAutorization);
            request.AddHeader("Cookie", _mauticCookie);
            IRestResponse response = client.Execute(request);
            return response.Content.ToString();
        }
    }
}
