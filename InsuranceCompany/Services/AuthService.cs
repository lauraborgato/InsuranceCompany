using System;
using System.Security.Claims;
using System.Text;

using InsuranceCompany.IServices;
using Microsoft.Extensions.Options;
using InsuranceCompany.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace InsuranceCompany.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppSettings _appSettings;
        private readonly IClientService _clientService;
        public AuthService(IClientService clientService, IOptions<AppSettings> appSettings)
        {
            _clientService = clientService;
            _appSettings = appSettings.Value;

        }
        public string Login(string email)
        {
            var client = _clientService.getClientByEmail(email);

            if (client == null)
                return null;
            
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier, client.Id),
                    new Claim(ClaimTypes.Email, client.Email),
                    new Claim(ClaimTypes.Role, client.Role)
                }),
                // Nuestro token va a durar un día
                Expires = DateTime.UtcNow.AddDays(1),
                // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }
    }
}