using System.Collections.Generic;
using System.Linq;
using InsuranceCompany.Models;

namespace InsuranceCompany.test
{
    public static class PolicyListStub
    {
        private static IEnumerable<Policy> policies = new List<Policy>()
        {
            new Policy()
            {
                Email="pol1@test.com",
                Id="aassaa",
                AmountInsured = 4320.00,
                ClientId="1234", 
                InceptionDate= "2016-06-01T03:33:32Z",
                InstallmentPayment = true
            },
            new Policy()
            {
                Email="pol2@test.com",
                Id="bbssbb",
                AmountInsured = 3789.00,
                ClientId="1234",
                InceptionDate= "2018-06-01T03:33:32Z",
                InstallmentPayment = true
            }
        };
        public static Policy GetFirst()
        {
            return policies.ToList().FirstOrDefault();
        }

        public static IEnumerable<Policy> GetAll()
        {
            return policies;
        }

        public static string GetStringData()
        {
            return "{ \"policies\":[ { \"id\":\"aassaa\",\"email\":\"pol1@test.com\",\"amountInsured\"" +
                ":\"4320.00\",\r\n \"clientId\":\"1234\"\r\n, \r\n \"InceptionDate\":\"2016-06-01T03:33:32Z\"\r\n, \r\n \"InstallmentPayment\":\"true\"\r\n }," +
                "{ \"id\":\"bbssbb\",\"email\":\"pol2@test.com\",\"amountInsured\"" +
                ":\"3789.00\",\r\n \"clientId\":\"1234\"\r\n, \r\n \"InceptionDate\":\"2018-06-01T03:33:32Z\"\r\n, \r\n \"InstallmentPayment\":\"true\"\r\n } \r\n] }";

        }
    }
}