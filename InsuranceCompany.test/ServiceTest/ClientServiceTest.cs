using InsuranceCompany.Services;
using Moq;
using NUnit.Framework;
using System.Net.Http;

namespace InsuranceCompany.test
{
    public class ClientServiceTest
    {
        private readonly ClientService _service;
        private readonly Mock<IHttpClientFactory> _httpClient = new Mock<IHttpClientFactory>();

        public ClientServiceTest()
        {
            var clientData = ClientListStub.GetStringData();
            var clientHandlerStub = new DelegatingHandlerStub(clientData);
            var client = new HttpClient(clientHandlerStub);

            _httpClient.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            _service = new ClientService(_httpClient.Object);
        }

        [Test]
        public void Get_Client_By_Email_Must_Return_A_Client()
        {
            var stubClient = ClientListStub.GetFirst();
            var client = _service.getClientByEmail(stubClient.Email);

            Assert.IsNotNull(client);
            Assert.AreEqual(client.Email, stubClient.Email);
            Assert.AreEqual(client.Id, stubClient.Id);
            Assert.AreEqual(client.Name, stubClient.Name);
            Assert.AreEqual(client.Role, stubClient.Role);
        }

        [Test]
        public void Get_Client_By_Email_Must_Return_Null()
        {
            var client = _service.getClientByEmail("notfound");

            Assert.IsNull(client);
        }

        [Test]
        public void Get_Client_By_Id_Must_Return_A_Client()
        {
            var stubClient = ClientListStub.GetFirst();

            var client = _service.getClientById(stubClient.Id);

            Assert.IsNotNull(client);
            Assert.AreEqual(client.Email, stubClient.Email);
            Assert.AreEqual(client.Id, stubClient.Id);
            Assert.AreEqual(client.Name, stubClient.Name);
            Assert.AreEqual(client.Role, stubClient.Role);
        }

        [Test]
        public void Get_Client_By_Id_Must_Return_Null()
        {
            var client = _service.getClientById("2345yegdye");

            Assert.IsNull(client);
        }
    }
}