using MauticApi.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MauticApi.Service
{
    public interface IService
    {
        public IRestResponse CreateContact(Contact contact, string _mauticAutorization, string _mauticCookie);
        public IRestResponse AddContactInSegment(Contact contact, string _mauticAutorization, string _mauticCookie);
    }
}
