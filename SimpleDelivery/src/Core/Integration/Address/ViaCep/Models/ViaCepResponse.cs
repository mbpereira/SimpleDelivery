using Newtonsoft.Json;

namespace Core.Integration.Address.ViaCep.Models
{
    internal class ViaCepResponse
    {
        [JsonProperty("cep")]
        public string PostalCode { get; set; }
        [JsonProperty("logradouro")]
        public string Street { get; set; }
        [JsonProperty("bairro")]
        public string District { get; set; }
        [JsonProperty("localidade")]
        public string City { get; set; }
        [JsonProperty("uf")]
        public string Uf { get; set; }
    }
}
