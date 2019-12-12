using System.Collections.Generic;
using System.Linq;
using InsuranceCompany.Models;

namespace InsuranceCompany.test
{
    public static class ClientListStub
    {
        private static IEnumerable<Client> clients = new List<Client>()
        {
            new Client()
            {
                Email="test@test.com",
                Id="1234",
                Name="test",
                Role = "admin"
            },
            new Client()
            {
                Email="test1@test.com",
                Id="1235",
                Name="test1",
                Role = "user"
            }
        };
        public static Client GetFirst()
        {
            return clients.ToList().FirstOrDefault();
        }

        public static IEnumerable<Client> GetAll()
        {
            return clients;
        }

        public static string GetStringData()
        {
            return "{ \"clients\":[ { \"id\":\"1234\",\"name\":\"test\",\"email\":\"test@test.com\",\r\n \"role\":\"admin\"\r\n },\r\n      {  \r\n \"id\":\"1235\",\r\n \"name\":\"test1\",\r\n \"email\":\"test1@test.com\",\r\n \"role\":\"user\"\r\n }\r\n] }";

        }
    }
}