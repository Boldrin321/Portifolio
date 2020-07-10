using API.Models;
using RestSharp;

namespace API.Octopus
{
    public class OctopusService : IMailList
    {
        public IRestResponse AddInList(Lead lead, string _OctopusKey, string _OctopusUrlFull)
        {
      
            var url = _OctopusUrlFull;
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            var apikey = _OctopusKey;
            var body = "{\n    \"api_key\": \""+apikey+"\",\n    \"email_address\": \""+lead.Email+"\",\n    \"status\": \"SUBSCRIBED\"\n}";
            request.AddParameter("undefined", body, ParameterType.RequestBody);
            return client.Execute(request);
        }
    }
}
