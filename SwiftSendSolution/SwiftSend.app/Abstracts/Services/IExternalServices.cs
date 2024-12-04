namespace SwiftSend.app.Abstracts.Services
{
    //To do fixing httpclient instantiating methods
    public interface IExternalServices
    {
        Task<HttpResponseMessage> Delete(string uri, string format);
        Task<TResult> Get<TResult>(string uri);
        Task<HttpResponseMessage> Post<TRequest>(string uri, TRequest request, string format);
        Task<HttpResponseMessage> Put<TRequest>(string uri, TRequest request, string format);
    }
}
