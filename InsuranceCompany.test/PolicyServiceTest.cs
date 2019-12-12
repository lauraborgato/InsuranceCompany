using System.Collections.Generic;
using System.Net.Http;
using InsuranceCompany.IServices;
using InsuranceCompany.Models;
using InsuranceCompany.Services;
using Moq;
using NUnit.Framework;

namespace InsuranceCompany.test
{
    class PolicyServiceTest
    {
        private readonly PolicyService _service;
        private readonly Mock<IHttpClientFactory> _httpClient = new Mock<IHttpClientFactory>();
        private readonly Mock<IClientService> _client = new Mock<IClientService>();

        public PolicyServiceTest()
        {
            var policyData = "{ \"policies\":[ {\"id\":\"64cceef9-3a01-49ae-a23b-3761b604800b\", \"amountInsured\":1825.89, \"email\":\"inesblankenship@quotezart.com\", \"inceptionDate\":\"2016 -06-01T03:33:32Z\", \"installmentPayment\":true, \"clientId\":\"a0ece5db-cd14-4f21-812f-966633e7be86\"}, { \"id\":\"7b624ed3-00d5-4c1b-9ab8-c265067ef58b\", \"amountInsured\":399.89, \"email\":\"inesblankenship@quotezart.com\", \"inceptionDate\":\"2015 -07-06T06:55:49Z\", \"installmentPayment\":true, \"clientId\":\"a0ece5db -cd14-4f21-812f-966633e7be86\" }, { \"id\":\"56b415d6-53ee-4481-994f-4bffa47b5239\", \"amountInsured\":2301.98, \"email\":\"inesblankenship@quotezart.com\", \"inceptionDate\":\"2014-12-01T05:53:13Z\", \"installmentPayment\":false, \"clientId\":\"e8fd159b-57c4-4d36-9bd7-a59ca13057bb\" }, { \"id\":\"6f514ec4-1726-4628-974d-20afe4da130c\", \"amountInsured\":697.04, \"email\":\"inesblankenship@quotezart.com\", \"inceptionDate\":\"2014-09-12T12:10:23Z\", \"installmentPayment\":false, \"clientId\":\"a0ece5db -cd14-4f21-812f-966633e7be86\" }]}";
            var clientHandlerStub = new DelegatingHandlerStub(policyData);
            var client = new HttpClient(clientHandlerStub);

            _httpClient.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            var clients = new List<Client>();

            _service = new PolicyService(_httpClient.Object, _client.Object);
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Get_Policy_By_Policy_Number_Must_Return_A_Policy()
        {
            _client.Setup(x => x.getClientByEmail("a0ece5db-cd14-4f21-812f-966633e7be86")).Returns(new Client()
            {
                Email = "britneyblankenship@quotezart.com",
                Id = "a0ece5db-cd14-4f21-812f-966633e7be86",
                Name = "Britney",
                Role = "admin"
            });

            var policy = _service.GetPolicyByPolicyNumber("64cceef9-3a01-49ae-a23b-3761b604800b");

            Assert.IsNotNull(policy);
            Assert.AreEqual(policy.Id, "64cceef9-3a01-49ae-a23b-3761b604800b");
            Assert.AreEqual(policy.Email, "inesblankenship@quotezart.com");
            Assert.AreEqual(policy.AmountInsured, 1825.89);
            Assert.AreEqual(policy.InstallmentPayment, true);
            Assert.AreEqual(policy.ClientId, "a0ece5db-cd14-4f21-812f-966633e7be86");
            Assert.AreEqual(policy.InceptionDate, "2016 -06-01T03:33:32Z");
        }

        [Test]
        public void Get_Policy_By_Policy_Number_Must_Return_Null()
        {
            _client.Setup(x => x.getClientByEmail("a0ece5db-cd14-4f21-812f-966633e7be86")).Returns(new Client()
            {
                Email = "britneyblankenship@quotezart.com",
                Id = "a0ece5db-cd14-4f21-812f-966633e7be86",
                Name = "Britney",
                Role = "admin"
            });

            var policy = _service.GetPolicyByPolicyNumber("1234566");

            Assert.IsNull(policy);
        }

        [Test]
        public void Get_Client_By_Policy_Number_Must_Return_A_Policy()
        {
            _client.Setup(x => x.getClientById("a0ece5db-cd14-4f21-812f-966633e7be86")).Returns(new Client()
            {
                Email = "britneyblankenship@quotezart.com",
                Id = "a0ece5db-cd14-4f21-812f-966633e7be86",
                Name = "Britney",
                Role = "admin"
            });

            var client = _service.getClientByPolicyNumber("64cceef9-3a01-49ae-a23b-3761b604800b");

            Assert.IsNotNull(client);
            Assert.AreEqual(client.Email, "britneyblankenship@quotezart.com");
            Assert.AreEqual(client.Id, "a0ece5db-cd14-4f21-812f-966633e7be86");
            Assert.AreEqual(client.Name, "Britney");
            Assert.AreEqual(client.Role, "admin");
        }

        [Test]
        public void Get_Client_By_Policy_Number_Must_Return_Null()
        {
            _client.Setup(x => x.getClientById("a0ece5db-cd14-4f21-812f-966633e7be86")).Returns(new Client()
            {
                Email = "britneyblankenship@quotezart.com",
                Id = "a0ece5db-cd14-4f21-812f-966633e7be86",
                Name = "Britney",
                Role = "admin"
            });

            var client = _service.getClientByPolicyNumber("6t868686");

            Assert.IsNull(client);
        }
    }
}
