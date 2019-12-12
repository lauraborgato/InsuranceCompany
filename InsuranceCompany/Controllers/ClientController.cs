using InsuranceCompany.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceCompany.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController: ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService) => _clientService = clientService;

        [HttpGet]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "All")]
        public IActionResult GetClientById([FromRoute] string id)
        {
           var client = _clientService.getClientById(id);

           if(client != null){
               return new OkObjectResult(client);
           }

           return new BadRequestObjectResult("Not found");
        }

        [HttpGet]
        [Route("name/{userName}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "All")]
        public IActionResult GetClientByUserName([FromRoute] string userName)
        {
           var client = _clientService.getClientByEmail(userName);

           if(client != null){
               return new OkObjectResult(client);
           }

           return new BadRequestObjectResult("Not found");
        }
    }
}