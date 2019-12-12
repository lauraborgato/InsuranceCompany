using System.Collections.Generic;
using System.Linq;
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
            var policyData = PolicyListStub.GetStringData();
            var clientHandlerStub = new DelegatingHandlerStub(policyData);
            var client = new HttpClient(clientHandlerStub);

            _httpClient.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            var clients = new List<Client>();

            _service = new PolicyService(_httpClient.Object, _client.Object);
        }

        [Test]
        public void Get_Policy_By_Policy_Number_Must_Return_A_Policy()
        {
            var stubPolicy = PolicyListStub.GetFirst();
            var policy = _service.GetPolicyByPolicyNumber(stubPolicy.Id);

            Assert.IsNotNull(policy);
            Assert.AreEqual(policy.Id, stubPolicy.Id);
            Assert.AreEqual(policy.Email, stubPolicy.Email);
            Assert.AreEqual(policy.AmountInsured, stubPolicy.AmountInsured);
            Assert.AreEqual(policy.InstallmentPayment, stubPolicy.InstallmentPayment);
            Assert.AreEqual(policy.ClientId, stubPolicy.ClientId);
        }

        [Test]
        public void Get_Policy_By_Policy_Number_Must_Return_Null()
        {
            var policy = _service.GetPolicyByPolicyNumber("1234566");

            Assert.IsNull(policy);
        }

        [Test]
        public void Get_Client_By_Policy_Number_Must_Return_A_Policy()
        {
            var stubClient = ClientListStub.GetFirst();
            var stubPolicy = PolicyListStub.GetFirst();

            _client.Setup(x => x.getClientById(stubClient.Id)).Returns(stubClient);

            var client = _service.getClientByPolicyNumber(stubPolicy.Id);

            Assert.IsNotNull(client);
            Assert.AreEqual(client.Email, stubClient.Email);
            Assert.AreEqual(client.Id, stubClient.Id);
            Assert.AreEqual(client.Name, stubClient.Name);
            Assert.AreEqual(client.Role, stubClient.Role);
        }

        [Test]
        public void Get_Client_By_Policy_Number_Must_Return_Null()
        {
            var stubClient = ClientListStub.GetFirst();

            _client.Setup(x => x.getClientById(stubClient.Id)).Returns(stubClient);

            var client = _service.getClientByPolicyNumber("6t868686");

            Assert.IsNull(client);
        }

        [Test]
        public void Get_List_Of_Policies_By_UserName_Must_Return_A_List()
        {
            var stubClient = ClientListStub.GetFirst();
            var stubPolicy = PolicyListStub.GetFirst();

            _client.Setup(x => x.getClientByEmail(stubClient.Email)).Returns(stubClient);

            var policies = _service.GetListOfPoliciesByUserName(stubClient.Email);

            Assert.IsNotNull(policies);
            Assert.IsTrue(policies.ToArray().Length > 0);
            Assert.AreEqual(policies.First().Id, stubPolicy.Id);
            Assert.AreEqual(policies.First().Email, stubPolicy.Email);
            Assert.AreEqual(policies.First().AmountInsured, stubPolicy.AmountInsured);
            Assert.AreEqual(policies.First().InstallmentPayment, stubPolicy.InstallmentPayment);
            Assert.AreEqual(policies.First().ClientId, stubPolicy.ClientId);
        }

        [Test]
        public void Get_List_Of_Policies_By_UserName_Must_Return_An_Empty_List()
        {
            var policies = _service.GetListOfPoliciesByUserName("nouser@quotezart.com");

            Assert.IsNull(policies);
        }
    }
}
