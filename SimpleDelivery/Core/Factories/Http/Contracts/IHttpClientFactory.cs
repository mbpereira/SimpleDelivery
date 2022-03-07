using System.Net.Http;

namespace Core.Factories.Http.Contracts
{
    public interface IHttpClientFactory
    {
        HttpClient Create();
    }
}
