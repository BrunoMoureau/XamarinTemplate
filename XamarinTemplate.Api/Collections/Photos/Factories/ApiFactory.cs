using System;
using System.Net.Http;
using Refit;
using XamarinTemplate.Api.Abstracts;

namespace XamarinTemplate.Api.Collections.Photos.Factories
{
    public class ApiFactory
    {
        private readonly Func<HttpMessageHandler> _httpMessageHandlerFactory;

        public ApiFactory(Func<HttpMessageHandler> httpMessageHandlerFactory)
        {
            _httpMessageHandlerFactory = httpMessageHandlerFactory;
        }
        
        public IPhotoApi CreatePhotoApi(string baseUrl)
        {
            var apiSettings = new RefitSettings(new NewtonsoftJsonContentSerializer())
            {
                HttpMessageHandlerFactory = _httpMessageHandlerFactory
            };
            
            return RestService.For<IPhotoApi>(baseUrl, apiSettings);
        }
    }
}