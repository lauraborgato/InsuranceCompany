using System.Threading.Tasks;

using InsuranceCompany.Models;

namespace InsuranceCompany.IServices
{
    public interface IClientService
    {
        Client getClientByEmail(string email);
        Client getClientById(string id);
        Task getClientList();
    }
}