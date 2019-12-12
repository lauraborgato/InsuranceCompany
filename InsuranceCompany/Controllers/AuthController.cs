using InsuranceCompany.IServices;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceCompany.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost]
        [Route("login")]
        public string Login([FromBody] string email)
        {
           var token = _authService.Login(email);

           if(token != null){
               return token;
           }

           return "Error: Unahutorized user";
        }
    }
}