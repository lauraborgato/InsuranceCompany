using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using InsuranceCompany.IServices;
using InsuranceCompany.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace InsuranceCompany.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IHttpClientFactory _httpClient;
        private IEnumerable<Policy> _policies;

        private readonly IClientService _clientService;

        public PolicyService(IHttpClientFactory httpClient, IClientService clientService)
        {
            _httpClient = httpClient;
            getPolicies().Wait();
            _clientService = clientService;
        }
        public IEnumerable<Policy> GetListOfPoliciesByUserName(string userName)
        {
            var client = _clientService.getClientByEmail(userName);
            return _policies.Where(policy => policy.ClientId.Equals(client.Id));
        }

        public Policy GetPolicyByPolicyNumber(string id) => _policies.Where(policy => policy.Id.Equals(id)).FirstOrDefault();

        private async Task getPolicies()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://www.mocky.io/v2/580891a4100000e8242b75c5");

            var client = _httpClient.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {

                var responseString = await response.Content.ReadAsStringAsync();

                var policyJson = JObject.Parse(responseString);


                var policyList = (JArray)policyJson["policies"];

                _policies = policyList.ToObject<IList<Policy>>();
            }
            else
            {
                _policies = Array.Empty<Policy>();
            }
        }

        public Client getClientByPolicyNumber(string policyNumber)
        {
            var policy = GetPolicyByPolicyNumber(policyNumber);
            return _clientService.getClientById(policy.ClientId);
        }
    }
}