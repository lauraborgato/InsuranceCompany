using InsuranceCompany.IServices;
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
            var clientData = "{ \"clients\":[ { \"id\":\"a0ece5db-cd14-4f21-812f-966633e7be86\",\"name\":\"Britney\",\"email\":\"britneyblankenship@quotezart.com\",\r\n         \"role\":\"admin\"\r\n      },\r\n      {  \r\n         \"id\":\"e8fd159b-57c4-4d36-9bd7-a59ca13057bb\",\r\n         \"name\":\"Manning\",\r\n         \"email\":\"manningblankenship@quotezart.com\",\r\n         \"role\":\"admin\"\r\n      },\r\n      {  \r\n         \"id\":\"a3b8d425-2b60-4ad7-becc-bedf2ef860bd\",\r\n         \"name\":\"Barnett\",\r\n         \"email\":\"barnettblankenship@quotezart.com\",\r\n         \"role\":\"user\"\r\n      }\r\n] }";
            var clientHandlerStub = new DelegatingHandlerStub(clientData);
            var client = new HttpClient(clientHandlerStub);

            _httpClient.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            _service = new ClientService(_httpClient.Object);
        }

        [Test]
        public void Get_Client_By_Email_Must_Return_A_Client()
        {
            var client = _service.getClientByEmail("britneyblankenship@quotezart.com");

            Assert.IsNotNull(client);
            Assert.AreEqual(client.Email, "britneyblankenship@quotezart.com");
            Assert.AreEqual(client.Id, "a0ece5db-cd14-4f21-812f-966633e7be86");
            Assert.AreEqual(client.Name, "Britney");
            Assert.AreEqual(client.Role, "admin");
        }

        [Test]
        public void Get_Client_By_Email_Must_Return_Null()
        {
            var client = _service.getClientByEmail("1234");

            Assert.IsNull(client);
        }

        [Test]
        public void Get_Client_By_Id_Must_Return_A_Client()
        {
            var client = _service.getClientById("a0ece5db-cd14-4f21-812f-966633e7be86");

            Assert.IsNotNull(client);
            Assert.AreEqual(client.Email, "britneyblankenship@quotezart.com");
            Assert.AreEqual(client.Id, "a0ece5db-cd14-4f21-812f-966633e7be86");
            Assert.AreEqual(client.Name, "Britney");
            Assert.AreEqual(client.Role, "admin");
        }

        [Test]
        public void Get_Client_By_Id_Must_Return_Null()
        {
            var client = _service.getClientById("2345yegdye");

            Assert.IsNull(client);
        }
    }
}