using API.Models;
using RestSharp;

namespace API.Octopus
{
    public interface IMailList
    {
        public IRestResponse AddInList(Lead lead, string _OctopusKey, string _OctopusUrlFull);
    }
}
