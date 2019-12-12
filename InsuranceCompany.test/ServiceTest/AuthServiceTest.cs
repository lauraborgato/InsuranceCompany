using InsuranceCompany.IServices;
using InsuranceCompany.Models;
using InsuranceCompany.Services;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace InsuranceCompany.test
{
    public class AuthServiceTest
    {
        private readonly AuthService _service;
        private readonly Mock<IClientService> _clientService = new Mock<IClientService>();
        private readonly Mock<IOptions<AppSettings>> _appSettings = new Mock<IOptions<AppSettings>>();

        public AuthServiceTest()
        {
            _service = new AuthService(_clientService.Object, _appSettings.Object);
        }

        [Test]
        public void Unauthorized()
        {
            var token = _service.Login("notfound");

            Assert.IsNull(token);
        }
    }
}