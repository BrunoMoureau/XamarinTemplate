// ReSharper disable once CheckNamespace
namespace MAUI.Template.Dependencies
{
    public partial class HttpMessageHandlerService
    {
        public partial Func<HttpMessageHandler> Create()
        {
            return () => new HttpClientHandler();
        }
    }
}