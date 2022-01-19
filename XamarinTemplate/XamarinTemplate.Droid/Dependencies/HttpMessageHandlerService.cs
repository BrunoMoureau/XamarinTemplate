using System;
using System.Net.Http;
using Xamarin.Android.Net;
using Xamarin.Forms;
using XamarinTemplate.Droid.Dependencies;
using XamarinTemplate.Services.HttpMessageHandlers;

[assembly: Dependency(typeof(HttpMessageHandlerService))]
namespace XamarinTemplate.Droid.Dependencies
{
    public class HttpMessageHandlerService : IHttpMessageHandlerService
    {
        public Func<HttpMessageHandler> Create()
        {
            return () => new AndroidClientHandler();
        }
    }
}