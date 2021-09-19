using System;

namespace XamarinTemplate.Services.HttpMessageHandler
{
    public interface IHttpMessageHandlerService
    {
        Func<System.Net.Http.HttpMessageHandler> Create();
    }
}