using InsuranceCompany.Controllers;
using InsuranceCompany.IServices;
using InsuranceCompany.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace InsuranceCompany.test
{
    class PolicyControllerTest
    {
        private readonly PolicyController _controller;
        private readonly Mock<IPolicyService> _service = new Mock<IPolicyService>();

        public PolicyControllerTest()
        {
            _controller = new PolicyController(_service.Object);
        }

        [Test]
        public void Get_Client_By_Id_Must_Return_Client()
        {
            var clientStub = ClientListStub.GetFirst();
            var stubPolicy = PolicyListStub.GetFirst();

            _service.Setup(x => x.GetListOfPoliciesByUserName(clientStub.Email)).Returns(PolicyListStub.GetAll());

            var result = _controller.GetPoliciesByUserName(clientStub.Email) as OkObjectResult;
            var policies = result.Value as IEnumerable<Policy>;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(policies.ToList().First().Id, stubPolicy.Id);
            Assert.AreEqual(policies.ToList().First().Email, stubPolicy.Email);
        }

        [Test]
        public void Get_Client_By_Id_Must_Return_Not_Found()
        {
            var result = _controller.GetPoliciesByUserName("notfoundemail") as BadRequestObjectResult;

            var message = result.Value as string;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(message, "Not found");
        }

        [Test]
        public void Get_Client_By_User_Name_Must_Return_Client()
        {
            var clientStub = ClientListStub.GetFirst();
            var stubPolicy = PolicyListStub.GetFirst();

            _service.Setup(x => x.getClientByPolicyNumber(stubPolicy.Id)).Returns(ClientListStub.GetFirst());

            var result = _controller.GetClientByPolicyNumber(stubPolicy.Id) as OkObjectResult;
            var client = result.Value as Client;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(client.Id, clientStub.Id);
            Assert.AreEqual(client.Email, clientStub.Email);
        }

        [Test]
        public void Get_Client_By_User_Name_Must_Return_Not_Found()
        {
            var result = _controller.GetClientByPolicyNumber("notFound") as BadRequestObjectResult;
            var message = result.Value as string;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(message, "Not found");
        }
    }
}
