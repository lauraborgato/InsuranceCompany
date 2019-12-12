using InsuranceCompany.Controllers;
using InsuranceCompany.IServices;
using InsuranceCompany.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace InsuranceCompany.test
{
    class ClientControllerTest
    {
        private readonly ClientController _controller;
        private readonly Mock<IClientService> _service = new Mock<IClientService>();

        public ClientControllerTest()
        {
            _controller = new ClientController(_service.Object);
        }

        [Test]
        public void Get_Client_By_Id_Must_Return_Client()
        {
            var clientStub = ClientListStub.GetFirst();

            _service.Setup(x => x.getClientById(clientStub.Id)).Returns(ClientListStub.GetFirst());

            var result = _controller.GetClientById(clientStub.Id) as OkObjectResult;
            var client = result.Value as Client;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(client.Id, clientStub.Id);
            Assert.AreEqual(client.Email, clientStub.Email);
        }

        [Test]
        public void Get_Client_By_Id_Must_Return_Not_Found()
        {
            var result = _controller.GetClientById("trtrtr") as BadRequestObjectResult;
            var message = result.Value as string;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(message, "Not found");
        }

        [Test]
        public void Get_Client_By_User_Name_Must_Return_Client()
        {
            var clientStub = ClientListStub.GetFirst();
            _service.Setup(x => x.getClientByEmail(clientStub.Email)).Returns(ClientListStub.GetFirst());

            var result = _controller.GetClientByUserName(clientStub.Email) as OkObjectResult;
            var client = result.Value as Client;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(client.Id, clientStub.Id);
            Assert.AreEqual(client.Email, clientStub.Email);
        }

        [Test]
        public void Get_Client_By_User_Name_Must_Return_Not_Found()
        {
            var result = _controller.GetClientByUserName("notFound") as BadRequestObjectResult;
            var message = result.Value as string;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(message, "Not found");
        }
    }
}
