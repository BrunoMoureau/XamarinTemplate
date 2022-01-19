using System;
using System.Net.Http;
using Xamarin.Forms;
using XamarinTemplate.iOS.Dependencies;
using XamarinTemplate.Services.HttpMessageHandlers;

[assembly: Dependency(typeof(HttpMessageHandlerService))]
namespace XamarinTemplate.iOS.Dependencies
{
    public class HttpMessageHandlerService : IHttpMessageHandlerService
    {
        public Func<HttpMessageHandler> Create()
        {
            return () => new NSUrlSessionHandler();
        }
    }
}