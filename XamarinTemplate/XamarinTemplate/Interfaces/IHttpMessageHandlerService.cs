using System;
using System.Net.Http;

namespace XamarinTemplate.Interfaces
{
    public interface IHttpMessageHandlerService
    {
        Func<HttpMessageHandler> Create();
    }
}