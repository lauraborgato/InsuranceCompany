using InsuranceCompany.Models;
using System.Threading.Tasks;

namespace InsuranceCompany.IServices
{
    public interface IClientService
    {
        Client getClientByEmail(string email);
        Client getClientById(string id);
        Task getClientList();
    }
}