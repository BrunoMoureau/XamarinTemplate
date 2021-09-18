using System;
using System.Net.Http;
using Xamarin.Forms;
using XamarinTemplate.Interfaces;
using XamarinTemplate.iOS.Dependencies;

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