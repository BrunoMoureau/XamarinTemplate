using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XamarinTemplate.Abstractions.Photos;
using XamarinTemplate.Abstractions.Photos.Models;
using XamarinTemplate.Api.Collections.Photos;
using XamarinTemplate.Api.Policies;

namespace XamarinTemplate.Repositories.Photos
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
            var photos = await Policies.Retry
                .ExecuteAsync(() => _photoApi.GetPhotosAsync(cancellationToken))
                .ConfigureAwait(false);
            
            cancellationToken.ThrowIfCancellationRequested();
            
            return photos.Select(p => new Photo
            {
                Id = p.Id,
                AlbumId = p.AlbumId,
                Title = p.Title,
                Url = $"https://picsum.photos/id/{new Random().Next(1, 1000)}/1000",
                ThumbnailUrl = $"https://picsum.photos/id/{new Random().Next(1, 1000)}/500"
            }).ToList();
        }
    }
}