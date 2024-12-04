using Newtonsoft.Json;
using SwiftSend.app.Abstracts.Services;
using System.Text;

namespace SwiftSend.infrastructure.Services
{
    public class ExternalServices : IExternalServices
    {
        private readonly IHttpClientFactory _clientFactory;

        public ExternalServices(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<HttpResponseMessage> Post<TRequest>(string uri, TRequest request,
            string format)
        {
            var client = _clientFactory.CreateClient();
            StringContent content = new StringContent(
                JsonConvert.SerializeObject(request),
                Encoding.UTF8,
                format);

            return await client.PostAsync(uri, content);
        }

        public async Task<HttpResponseMessage> Put<TRequest>(string uri, TRequest request,
            string format)
        {
            var client = _clientFactory.CreateClient();
            StringContent content = new StringContent(
                JsonConvert.SerializeObject(request),
                Encoding.UTF8,
                format);

            return await client.PutAsync(uri, content);
        }

        public async Task<HttpResponseMessage> Delete(string uri,
            string format)
        {
            var client = _clientFactory.CreateClient();
            return await client.DeleteAsync(uri);
        }

        public async Task<TResult> Get<TResult>(string uri)
        {
            var client = _clientFactory.CreateClient();
            var result = await client.GetStringAsync(uri);
            var responseDeserialized = JsonConvert.DeserializeObject<TResult>(result);
            return responseDeserialized;
        }
    }
}
