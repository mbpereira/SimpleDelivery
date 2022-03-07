using Core.Factories.Http.Contracts;
using Core.Integration.Address.Contracts;
using Core.Integration.Address.ViaCep.Models;
using Core.Orders.Shipment.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Core.Integration.Address.ViaCep
{
    public class AddressGetter : IAddressGetter
    {
        private readonly string _url;
        private readonly IHttpClientFactory _httpClientFactory;

        public AddressGetter(IHttpClientFactory httpClientFactory)
        {
            _url = "https://viacep.com.br/ws/{0}/json/";
            _httpClientFactory = httpClientFactory;
        }

        public async Task<AddressInfo> GetAddress(string postalCode)
        {
            var url = string.Format(_url, postalCode);

            using (var httpClient = _httpClientFactory.Create())
            {
                var response = await httpClient.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var viaCepAddress = JsonConvert.DeserializeObject<ViaCepResponse>(json);

                return ConvertToAddress(viaCepAddress);
            }
        }

        private static AddressInfo ConvertToAddress(ViaCepResponse viaCepAddress)
        {
            return new AddressInfo()
            {
                AddressStreet = viaCepAddress.Street,
                City = viaCepAddress.City,
                PostalCode = viaCepAddress.PostalCode,
                Uf = viaCepAddress.Uf,
            };
        }
    }
}
