using System.Collections.Generic;

using InsuranceCompany.Models;

namespace InsuranceCompany.IServices
{
    public interface IPolicyService
    {
        IEnumerable<Policy> GetListOfPoliciesByUserName(string userName);
        Policy GetPolicyByPolicyNumber(string id);
        Client getClientByPolicyNumber(string policyNumber);
    }
}