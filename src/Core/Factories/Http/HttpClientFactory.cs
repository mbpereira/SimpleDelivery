using Core.Factories.Http.Contracts;
using System.Net.Http;

namespace Core.Factories.Http
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClient Create()
        {
            return new HttpClient();
        }
    }
}
