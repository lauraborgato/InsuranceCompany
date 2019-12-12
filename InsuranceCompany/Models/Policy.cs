using Newtonsoft.Json;

namespace InsuranceCompany.Models
{
    public class Policy
    {
        [JsonProperty("id")]

        public string Id { get; set; }
        [JsonProperty("amountInsured")]

        public double AmountInsured { get; set; }
        [JsonProperty("email")]

        public string Email { get; set; }
        [JsonProperty("inceptionDate")]

        public string InceptionDate { get; set; }
        [JsonProperty("InstallmentPayment")]
        public bool InstallmentPayment { get; set; }
        [JsonProperty("clientId")]

        public string ClientId { get; set; }
    }
}