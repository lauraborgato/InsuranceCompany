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
    class AuthControllerTest
    {
        private readonly AuthController _controller;
        private readonly Mock<IAuthService> _service = new Mock<IAuthService>();

        public AuthControllerTest()
        {
            _controller = new AuthController(_service.Object);
        }

        [Test]
        public void Login()
        {
            var clientStub = ClientListStub.GetFirst();

            _service.Setup(x => x.Login(clientStub.Email)).Returns("NewauthToken");

            var result = _controller.Login(clientStub.Email);

            Assert.AreEqual(result, "NewauthToken");
        }

        [Test]
        public void Unahutorized()
        {
            var result = _controller.Login("notfound");

            Assert.AreEqual(result, "Error: Unahutorized user");
        }
    }
}
