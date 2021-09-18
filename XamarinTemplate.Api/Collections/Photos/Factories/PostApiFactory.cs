using System;
using System.Net.Http;
using Refit;

namespace XamarinTemplate.Api.Collections.Photos.Factories
{
    public class PhotoApiFactory : IPhotoApiFactory
    {
        private readonly Func<HttpMessageHandler> _httpMessageHandlerFactory;

        public PhotoApiFactory(Func<HttpMessageHandler> httpMessageHandlerFactory)
        {
            _httpMessageHandlerFactory = httpMessageHandlerFactory;
        }
        
        public IPhotoApi Create(string baseUrl)
        {
            var apiSettings = new RefitSettings(new NewtonsoftJsonContentSerializer())
            {
                HttpMessageHandlerFactory = _httpMessageHandlerFactory
            };
            
            return RestService.For<IPhotoApi>(baseUrl, apiSettings);
        }
    }
}