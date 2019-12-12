
using Newtonsoft.Json;

namespace InsuranceCompany.Models
{
    public class Client
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        
        public string Email { get; set; }
        [JsonProperty("role")]
        
        public string Role { get; set; }
    }
}