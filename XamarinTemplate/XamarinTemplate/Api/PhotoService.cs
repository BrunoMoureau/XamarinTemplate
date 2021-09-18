using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XamarinTemplate.Api.Collections.Photos;
using XamarinTemplate.Views.List.Models;

namespace XamarinTemplate.Api
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoApi _photoApi;

        public PhotoService(IPhotoApi photoApi)
        {
            _photoApi = photoApi;
        }
        
        public async Task<List<Photo>> GetPhotosAsync(CancellationToken cancellationToken)
        {
            var photos = await _photoApi.GetPhotosAsync(cancellationToken);
            return photos.Select(p => new Photo
            {
                Id = p.Id,
                AlbumId = p.AlbumId,
                Title = p.Title,
                Url = p.Url,
                ThumbnailUrl = p.ThumbnailUrl
            }).ToList();
        }
    }
}