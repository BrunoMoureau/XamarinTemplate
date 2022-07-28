using MAUI.Template.Abstractions.Photos;
using MAUI.Template.Abstractions.Photos.Models;
using MAUI.Template.Api.Collections.Photos;
using MAUI.Template.Api.Policies;

namespace MAUI.Template.Repositories.Photos
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