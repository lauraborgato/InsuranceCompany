using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using InsuranceCompany.IServices;

namespace InsuranceCompany.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService) => _policyService = policyService;

        [HttpGet]
        [Route("{userName}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "Admin")]
        public IActionResult GetPoliciesByUserName([FromRoute] string userName)
        {
            var policies = _policyService.GetListOfPoliciesByUserName(userName);

            if (policies != null && policies.Count() > 0)
            {
                return new OkObjectResult(policies);
            }

            return new BadRequestObjectResult("Not found");
        }

        [HttpGet]
        [Route("client/{policyNumber}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "Admin")]
        public IActionResult GetClientByPolicyNumber([FromRoute] string policyNumber)
        {
            var client = _policyService.getClientByPolicyNumber(policyNumber);

            if (client != null)
            {
                return new OkObjectResult(client);
            }

            return new BadRequestObjectResult("Not found");
        }
    }
}