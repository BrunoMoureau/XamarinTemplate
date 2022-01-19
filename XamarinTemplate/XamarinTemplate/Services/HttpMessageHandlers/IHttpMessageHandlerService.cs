using System;
using System.Net.Http;

namespace XamarinTemplate.Services.HttpMessageHandlers
{
    public interface IHttpMessageHandlerService
    {
        Func<HttpMessageHandler> Create();
    }
}