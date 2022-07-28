namespace MAUI.Template.Services.HttpMessageHandlers
{
    public interface IHttpMessageHandlerService
    {
        Func<HttpMessageHandler> Create();
    }
}