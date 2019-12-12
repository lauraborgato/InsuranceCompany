using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using InsuranceCompany.IServices;
using InsuranceCompany.Models;

using Newtonsoft.Json.Linq;

namespace InsuranceCompany.Services
{
    public class ClientService : IClientService
    {
        private readonly IHttpClientFactory _httpClient;
        private IEnumerable<Client> _clients;

        public ClientService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            getClientList().Wait();
        }
        public Client getClientByEmail(string email) => _clients.Where(client => client.Email.Equals(email)).FirstOrDefault();

        public Client getClientById(string id) => _clients.Where(client => client.Id.Equals(id)).FirstOrDefault();

        public async Task getClientList()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://www.mocky.io/v2/5808862710000087232b75ac");

            var client = _httpClient.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {

                var responseString = await response.Content.ReadAsStringAsync();

                var clientJson = JObject.Parse(responseString);


                var clientList = (JArray)clientJson["clients"];
                
                _clients = clientList.ToObject<IList<Client>>();
            }
            else
            {
                _clients = Array.Empty<Client>();
            }
        }
    }
}